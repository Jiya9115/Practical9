using Microsoft.Data.SqlClient;

namespace PlacementDashboard.Data;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection is missing in appsettings.json.");
    }

    public SqlConnection Create()
    {
        return new SqlConnection(_connectionString);
    }
}
