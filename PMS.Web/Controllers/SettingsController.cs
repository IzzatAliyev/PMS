using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public SettingsController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}