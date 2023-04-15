using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Project;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectViewModel project)
        {
            await this.projectService.CreateProject(project);
            return this.Created(nameof(CreateProject), "Successfully created!");
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateProjectById([FromRoute] int id, [FromBody] ProjectViewModel project)
        {
            try
            {
                await this.projectService.UpdateProjectById(id, project);
                return this.Created(nameof(UpdateProjectById), "Successfully updated!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProjectById([FromRoute] int id)
        {
            try
            {
                await this.projectService.DeleteProjectById(id);
                return this.Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectById([FromRoute] int id)
        {
            try
            {
                var project = await this.projectService.GetProjectById(id);
                return this.Ok(project);
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
                var projects = this.projectService.GetAllProjects();
                return this.Ok(projects);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }
    }
}