using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;

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

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeViewModel employee)
        {
            await this.employeeService.CreateEmployee(employee);
            return this.Created(nameof(CreateEmployee), "Successfully created!");
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateEmployeeById([FromRoute] int id, [FromBody] EmployeeViewModel employee)
        {
            try
            {
                await this.employeeService.UpdateEmployeeById(id, employee);
                return this.Created(nameof(UpdateEmployeeById), "Successfully updated!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployeeById([FromRoute] int id)
        {
            try
            {
                await this.employeeService.DeleteEmployeeById(id);
                return this.Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                var employee = await this.employeeService.GetEmployeeById(id);
                return this.Ok(employee);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var employees = this.employeeService.GetAllEmployees();
                return this.Ok(employees);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("employee")]
        public IActionResult GetEmployeeByEmail([FromQuery] string email)
        {
            try
            {
                var employee = this.employeeService.GetEmployeeByEmail(email);
                return this.Ok(employee);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("id")]
        public IActionResult GetEmployeeIdByEmail([FromQuery] string email)
        {
            try
            {
                var employeeId = this.employeeService.GetEmployeeIdByEmail(email);
                return this.Ok(employeeId);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("name")]
        public IActionResult GetEmployeeByName([FromQuery] string name)
        {
            try
            {
                var employee = this.employeeService.GetEmployeeByName(name);
                return this.Ok(employee);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }
    }
}