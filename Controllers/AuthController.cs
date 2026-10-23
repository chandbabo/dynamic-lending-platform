using LendingProcessingSystem.Models;
using LendingProcessingSystem.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LendingProcessingSystem.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public AuthController(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            ModelState.AddModelError(string.Empty, "Email is required.");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = Url.Action(nameof(ResetPassword), "Auth", new { token, email = user.Email }, Request.Scheme);

        var body = $@"
            <html>
            <body>
                <p>Hello,</p>
                <p>You requested to reset your password.</p>
                <p><a href='{callbackUrl}'>Click here to reset your password</a></p>
            </body>
            </html>";

        await _emailSender.SendEmailAsync(user.Email!, "Reset Your Password", body);

        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(email))
        {
            return BadRequest();
        }

        return View(new ResetPasswordViewModel { Email = email, Token = token });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.NewPassword != model.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Password confirmation mismatch.");
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found.");
            return View(model);
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (result.Succeeded)
            return RedirectToAction(nameof(ResetPasswordConfirmation));

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}
