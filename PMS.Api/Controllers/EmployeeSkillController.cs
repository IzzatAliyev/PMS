using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/employee-skill")]
    public class EmployeeSkillController : ControllerBase
    {
         private readonly IEmployeeSkillService employeeSkillService;
        private readonly ILogger<EmployeeSkillController> logger;

        public EmployeeSkillController(IEmployeeSkillService employeeSkillService, ILogger<EmployeeSkillController> logger)
        {
            this.employeeSkillService = employeeSkillService;
            this.logger = logger;
        }
        
        [HttpGet("employee/{employeeId:int}/skills")]
        public IActionResult GetSkillsByEmployeeId([FromRoute] int employeeId)
        {
            var skills = this.employeeSkillService.GetSkillsByEmployeeId(employeeId);
            return this.Ok(skills);
        }
    }
}