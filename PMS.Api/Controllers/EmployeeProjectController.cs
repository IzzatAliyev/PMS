using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/employee-project")]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectService employeeProjectService;
        private readonly ILogger<EmployeeProjectController> logger;

        public EmployeeProjectController(IEmployeeProjectService employeeProjectService, ILogger<EmployeeProjectController> logger)
        {
            this.employeeProjectService = employeeProjectService;
            this.logger = logger;
        }

        [HttpGet("project/{projectId:int}/employees")]
        public IActionResult GetEmployeesByProjectId([FromRoute] int projectId)
        {
            if (projectId == 0)
            {
                return this.BadRequest("NotFound");
            }
            var employees = this.employeeProjectService.GetEmployeesByProjectId(projectId);
            return this.Ok(employees);
        }

        [HttpGet("employee/{employeeId:int}/projects")]
        public IActionResult GetProjectsByEmployeeId([FromRoute] int employeeId)
        {
            if (employeeId == 0)
            {
                return this.BadRequest("NotFound");
            }
            var projects = this.employeeProjectService.GetProjectsByEmployeeId(employeeId);
            return this.Ok(projects);
        }
    }
}