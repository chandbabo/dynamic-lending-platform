using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendingProcessingSystem.Controllers;

[Authorize(Roles = "Admin")]
public class SchedulerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(string jobName, string cronExpression, string jobType)
    {
        TempData["Message"] = "Scheduled job created successfully.";
        return RedirectToAction(nameof(Index));
    }
}
