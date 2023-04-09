using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Web.Controllers
{
    public class BoardController : Controller
    {
        private readonly IProjectService projectService;
        private readonly ILogger<BoardController> logger;

        public BoardController(IProjectService projectService, ILogger<BoardController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var projects = this.projectService.GetAllProjects();
            return View(projects);
        }
    }
}