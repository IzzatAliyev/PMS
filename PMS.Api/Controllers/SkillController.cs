using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Skill;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService skillService;
        private readonly ILogger<SkillController> logger;

        public SkillController(ISkillService skillService, ILogger<SkillController> logger)
        {
            this.skillService = skillService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] SkillViewModel skill)
        {
            await this.skillService.CreateSkill(skill);
            return this.Created(nameof(CreateSkill), "Successfully created!");
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateSkillById([FromRoute] int id, [FromBody] SkillViewModel skill)
        {
            try
            {
                await this.skillService.UpdateSkillById(id, skill);
                return this.Created(nameof(UpdateSkillById), "Successfully updated!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSkillById([FromRoute] int id)
        {
            try
            {
                await this.skillService.DeleteSkillById(id);
                return this.Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSkillById([FromRoute] int id)
        {
            try
            {
                var skill = await this.skillService.GetSkillById(id);
                return this.Ok(skill);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("name")]
        public IActionResult GetSkillByName([FromQuery] string name)
        {
            try
            {
                var skill = this.skillService.GetSkillByName(name);
                return this.Ok(skill);
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
                var skills = this.skillService.GetAllSkills();
                return this.Ok(skills);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }
    }
}