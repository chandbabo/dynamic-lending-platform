using LendingProcessingSystem.Models;
using LendingProcessingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LendingProcessingSystem.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserManagementService _userManagementService;

    public UsersController(UserManager<ApplicationUser> userManager, IUserManagementService userManagementService)
    {
        _userManager = userManager;
        _userManagementService = userManagementService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManagementService.GetUsersAsync();
        return View(users);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string email, string firstName, string lastName, string phone, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            ModelState.AddModelError(string.Empty, "Email is required.");
            return View();
        }

        if (await _userManager.FindByEmailAsync(email) != null)
        {
            ModelState.AddModelError(string.Empty, "This email already exists.");
            return View();
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phone
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View();
        }

        await _userManagementService.CreateUserAsync(user.Id, email, firstName, lastName, phone, User.Identity?.Name ?? "system");

        return RedirectToAction(nameof(Index));
    }
}
