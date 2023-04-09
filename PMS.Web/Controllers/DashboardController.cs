using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Web.Controllers
{
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
    }
}