using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTasksById()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var projects = this.projectService.GetAllProjects();
            foreach(var proj in projects)
            {
                Console.WriteLine(proj.Name);
            }
            return View(projects);
        }
    }
}