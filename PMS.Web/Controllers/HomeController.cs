using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Models;
using System.Web;

namespace PMS.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        bool loggedin = true;
        if (!loggedin)
        {
           return RedirectToAction("Login", "Auth");
        }
        else
        {
            return RedirectToAction("Index", "Dashboard");
        }

        // return View();
    }

    public new IActionResult NotFound()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
