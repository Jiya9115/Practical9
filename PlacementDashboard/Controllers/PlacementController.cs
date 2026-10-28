using Microsoft.AspNetCore.Mvc;
using PlacementDashboard.Data;
using PlacementDashboard.Models;

namespace PlacementDashboard.Controllers;

public class PlacementController : Controller
{
    private readonly PlacementRepository _repository;

    public PlacementController(PlacementRepository repository)
    {
        _repository = repository;
    }

    // Dashboard page: summary cards, filtered records, company-wise count
    [HttpGet]
    public async Task<IActionResult> Index(string? status)
    {
        var model = await _repository.GetDashboardAsync(PlacementStatuses.Normalise(status));
        return View(model);
    }

    // Printable report (alternative to Crystal Reports): browser Print -> Save as PDF
    [HttpGet]
    public async Task<IActionResult> Report(string? status)
    {
        var model = await _repository.GetDashboardAsync(PlacementStatuses.Normalise(status));
        return View(model);
    }

    public IActionResult Error()
    {
        return View("Error");
    }
}
