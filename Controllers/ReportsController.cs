using LendingProcessingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingProcessingSystem.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> Index()
    {
        var table = await _reportService.ExecuteReportAsync("usp_Report_CaseSummary");
        return View(table);
    }
}
