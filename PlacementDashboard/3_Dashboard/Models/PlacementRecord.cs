namespace PlacementDashboard.Models;

public class PlacementRecord
{
    public string EnrollmentNo { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string JobRole { get; set; } = string.Empty;
    public decimal PackageLPA { get; set; }
    public string PlacementStatus { get; set; } = string.Empty;
    public DateTime? PlacementDate { get; set; }

    public string StatusCss => PlacementStatus switch
    {
        PlacementStatuses.Placed => "good",
        PlacementStatuses.InProcess => "warning",
        _ => "danger"
    };
}
