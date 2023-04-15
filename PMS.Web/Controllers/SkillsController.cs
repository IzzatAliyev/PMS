using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.Controllers
{
    [Route("skills")]
    public class SkillsController : Controller
    {
        private readonly ILogger<SkillsController> _logger;

        public SkillsController(ILogger<SkillsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}