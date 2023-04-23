using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;
using PMS.Service.ViewModels.Auth;

namespace PMS.Web.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly PMSDbContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthController(PMSDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet("login")]
        public async Task<ActionResult> LoginAsync(string? returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var loginModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(loginModel);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Your authentication details are incorrect. Please try again.");
                    return View(model);
                }

                var result = await this.signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Your authentication details are incorrect. Please try again.");
                }
            }

            return View(model);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = $"{model.FirstName} {model.LastName}",
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await this.userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    this.context.Employees.Add(new Employee
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Position = EmployeePosition.Intern,
                        Email = user.Email,
                        Description = string.Empty,
                        PhoneNumber = string.Empty,
                        ProfilePicture = string.Empty
                    });

                    this.userManager.AddToRoleAsync(user, "Employee").Wait();
                    await this.context.SaveChangesAsync();
                    await this.signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}