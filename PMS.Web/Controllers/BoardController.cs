using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    public class BoardController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public BoardController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}