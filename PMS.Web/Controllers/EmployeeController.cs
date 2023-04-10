using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Web.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IEmployeeSkillService employeeSkillService;
        private readonly IEmployeeProjectService employeeProjectService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeSkillService employeeSkillService, IEmployeeProjectService employeeProjectService, IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            this.employeeSkillService = employeeSkillService;
            this.employeeProjectService = employeeProjectService;
            this.logger = logger;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            if (id == 0)
            {
                return RedirectToAction("NotFound", "Home");
            }
            var employee = await this.employeeService.GetEmployeeById(id);
            ViewBag.Skills = this.employeeSkillService.GetSkillsByEmployeeId(id);
            ViewBag.Projects = this.employeeProjectService.GetProjectsByEmployeeId(id);
            return View(employee);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = this.employeeService.GetAllEmployees();
            return View(employees);
        }
    }
}