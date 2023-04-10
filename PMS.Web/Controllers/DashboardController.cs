using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeProject;

namespace PMS.Web.Controllers
{
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IProjectService projectService;
        private readonly ILogger<DashboardController> logger;

        public DashboardController(ILogger<DashboardController> logger, IEmployeeService employeeService, IProjectService projectService)
        {
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Projects = this.projectService.GetAllProjects();
            ViewBag.Employees = this.employeeService.GetAllEmployees();
            return View();
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string query)
        {
            var queryLowerCase = query.ToLower();
            var projects = this.projectService.GetAllProjects();
            var employees = this.employeeService.GetAllEmployees();
            var matchingProjects = projects.Where(p => p.Name.ToLower().Contains(query) || p.Description.ToLower().Contains(query)).Select(p => new EmployeeProjectSearchViewModel
            {
                Name = p.Name,
                Url = $"http://localhost:5047/projects/{p.Id}",
                Type = "project"
            }).ToList();
            var matchingEmployees = employees.Where(e => e.Name.ToLower().Contains(query)).Select(e => new EmployeeProjectSearchViewModel
            {
                Name = e.Name,
                Url = $"http://localhost:5047/employees/{e.Id}",
                Type = "employee"
            }).ToList();

            var suitableData = matchingProjects.Concat(matchingEmployees).ToList();
            
            return new JsonResult(suitableData);
        }
    }
}