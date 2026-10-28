namespace PlacementDashboard.Models;

public class PlacementDashboardViewModel
{
    public int TotalStudents { get; set; }
    public int PlacedStudents { get; set; }
    public int TotalCompanies { get; set; }
    public decimal AveragePackage { get; set; }
    public string StatusFilter { get; set; } = PlacementStatuses.All;
    public List<PlacementRecord> Placements { get; set; } = new();
    public List<CompanySummary> Companies { get; set; } = new();
}
