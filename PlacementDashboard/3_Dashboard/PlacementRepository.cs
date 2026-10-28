using System.Data;
using Microsoft.Data.SqlClient;
using PlacementDashboard.Models;

namespace PlacementDashboard.Data;

public class PlacementRepository
{
    private readonly DatabaseConnection _database;

    public PlacementRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public async Task<PlacementDashboardViewModel> GetDashboardAsync(string status = PlacementStatuses.All)
    {
        var model = new PlacementDashboardViewModel { StatusFilter = status };

        await using var connection = _database.Create();
        await connection.OpenAsync();

        await LoadSummaryAsync(connection, model);
        model.Placements = await GetPlacementsAsync(connection, status);
        model.Companies = await GetCompanySummaryAsync(connection);

        return model;
    }

    private static async Task LoadSummaryAsync(SqlConnection connection, PlacementDashboardViewModel model)
    {
        const string sql = @"
            SELECT (SELECT COUNT(*) FROM dbo.Student)                                    AS TotalStudents,
                   (SELECT COUNT(DISTINCT StudentId) FROM dbo.Placement
                    WHERE PlacementStatus = 'Placed')                                    AS PlacedStudents,
                   (SELECT COUNT(*) FROM dbo.Company)                                    AS TotalCompanies,
                   (SELECT CAST(ISNULL(AVG(PackageLPA), 0) AS DECIMAL(8,2))
                    FROM dbo.Placement WHERE PlacementStatus = 'Placed')                 AS AveragePackage;";

        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            model.TotalStudents = reader.GetInt32(0);
            model.PlacedStudents = reader.GetInt32(1);
            model.TotalCompanies = reader.GetInt32(2);
            model.AveragePackage = reader.GetDecimal(3);
        }
    }

    private static async Task<List<PlacementRecord>> GetPlacementsAsync(SqlConnection connection, string status)
    {
        const string sql = @"
            SELECT s.EnrollmentNo, s.StudentName, s.Department, c.CompanyName, c.JobRole,
                   p.PackageLPA, p.PlacementStatus, p.PlacementDate
            FROM   dbo.Placement p
            JOIN   dbo.Student s ON s.StudentId = p.StudentId
            JOIN   dbo.Company c ON c.CompanyId = p.CompanyId
            WHERE  @Status = 'All' OR p.PlacementStatus = @Status
            ORDER  BY s.StudentName;";

        var records = new List<PlacementRecord>();

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = status;

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            records.Add(new PlacementRecord
            {
                EnrollmentNo = reader.GetString(0),
                StudentName = reader.GetString(1),
                Department = reader.GetString(2),
                CompanyName = reader.GetString(3),
                JobRole = reader.GetString(4),
                PackageLPA = reader.GetDecimal(5),
                PlacementStatus = reader.GetString(6),
                PlacementDate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7)
            });
        }

        return records;
    }

    private static async Task<List<CompanySummary>> GetCompanySummaryAsync(SqlConnection connection)
    {
        const string sql = @"
            SELECT c.CompanyName, c.JobRole,
                   SUM(CASE WHEN p.PlacementStatus = 'Placed' THEN 1 ELSE 0 END) AS PlacedCount
            FROM   dbo.Company c
            LEFT JOIN dbo.Placement p ON p.CompanyId = c.CompanyId
            GROUP  BY c.CompanyName, c.JobRole
            ORDER  BY c.CompanyName;";

        var companies = new List<CompanySummary>();

        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            companies.Add(new CompanySummary
            {
                CompanyName = reader.GetString(0),
                JobRole = reader.GetString(1),
                PlacedCount = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
            });
        }

        return companies;
    }
}
