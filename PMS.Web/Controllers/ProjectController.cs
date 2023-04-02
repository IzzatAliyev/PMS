using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTasksById()
        {
            return View();
        }
    }
}