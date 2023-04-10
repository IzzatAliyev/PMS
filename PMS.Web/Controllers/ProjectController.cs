using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Web.Controllers
{
    [Route("projects")]
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IEmployeeProjectService employeeProjectService;
        private readonly IProjectTaskService projectTaskService;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectService projectService, IEmployeeProjectService employeeProjectService, IProjectTaskService projectTaskService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.employeeProjectService = employeeProjectService;
            this.projectTaskService = projectTaskService;
            this.logger = logger;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            if (id == 0)
            {
                return RedirectToAction("NotFound", "Home");
            }
            var project = await this.projectService.GetProjectById(id);
            ViewBag.Employees = this.employeeProjectService.GetEmployeesByProjectId(id);
            return View(project);
        }

        [HttpGet("tasks/{id:int}")]
        public IActionResult GetTasksByProjectId([FromRoute] int id)
        {
            var tasks = this.projectTaskService.GetTasksByProjectId(id);
            return View(tasks);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = this.projectService.GetAllProjects();
            return View(projects);
        }
    }
}