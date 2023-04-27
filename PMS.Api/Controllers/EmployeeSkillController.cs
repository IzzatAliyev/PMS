using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeSkill;

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

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeSkill([FromBody] EmployeeSkillViewModel employeeSkill)
        {
            await this.employeeSkillService.CreateEmployeeSkill(employeeSkill);
            return this.Created(nameof(CreateEmployeeSkill), "Successfully created!");
        }

         [HttpDelete("duplicate")]
        public async Task<IActionResult> DeleteDuplicateEmployeeSkills()
        {
            await this.employeeSkillService.DeleteDuplicateEmployeeSkills();
            return this.Ok("Successfully deleted!");
        }
    }
}