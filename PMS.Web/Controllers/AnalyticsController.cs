using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    [Route("analytics")]
    public class AnalyticsController : Controller
    {
        private readonly ILogger<AnalyticsController> _logger;

        public AnalyticsController(ILogger<AnalyticsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}