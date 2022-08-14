using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.MVC.Controllers;
public class UsersController : Controller
{
    private readonly IAuthenticationService _authService;

    public UsersController(IAuthenticationService authService)
    {
        _authService = authService;
    }
    public IActionResult Login(string returnUrl = null)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM login, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authService.Authenticate(login.Email, login.Password);
            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }
        }

        ModelState.AddModelError("", "Log in attempt failed. Please try again");
        return View(login);
    }

    [HttpPost]
    public async Task<IActionResult> Logout(string returnUrl)
    {
        returnUrl ??= Url.Content("~/");
        await _authService.Logout();
        return LocalRedirect(returnUrl);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registration)
    {
        if (ModelState.IsValid)
        {
            var returnUrl = Url.Content("~/");
            var isCreated = await _authService.Register(registration);
            if (isCreated)
            {
                return LocalRedirect(returnUrl);
            }
        }
        ModelState.AddModelError("", "Registration attempt failed. Please try again.");
        return View(registration);
    }
}
