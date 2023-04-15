using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
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
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            if (id == 0)
            {
                return this.BadRequest("NotFound");
            }
            var employee = await this.employeeService.GetEmployeeById(id);
            return this.Ok(employee);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = this.employeeService.GetAllEmployees();
            return this.Ok(employees);
        }
    }
}