using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectById([FromRoute] int id)
        {
            if (id == 0)
            {
                return this.BadRequest("NotFound");
            }
            var project = await this.projectService.GetProjectById(id);
            return this.Ok(project);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = this.projectService.GetAllProjects();
            return this.Ok(projects);
        }
    }
}