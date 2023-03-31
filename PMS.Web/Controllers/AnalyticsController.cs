using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AnalyticsController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}