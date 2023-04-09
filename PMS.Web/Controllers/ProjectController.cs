using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IEmployeeProjectService employeeProjectService;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectService projectService, IEmployeeProjectService employeeProjectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.employeeProjectService = employeeProjectService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("NotFound", "Home");
            }
            var project = await this.projectService.GetProjectById(id);
            ViewBag.Employees = this.employeeProjectService.GetEmployeesByProjectId(id);
            return View(project);
        }

        public IActionResult GetTasksById()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var projects = this.projectService.GetAllProjects();
            return View(projects);
        }
    }
}