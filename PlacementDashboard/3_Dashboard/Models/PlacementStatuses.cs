namespace PlacementDashboard.Models;

public static class PlacementStatuses
{
    public const string All = "All";
    public const string Placed = "Placed";
    public const string InProcess = "In Process";
    public const string NotPlaced = "Not Placed";

    public static readonly string[] Filters = { All, Placed, InProcess, NotPlaced };

    public static string Normalise(string? status)
    {
        return status is not null && Filters.Contains(status) ? status : All;
    }
}
