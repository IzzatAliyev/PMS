using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/project-task")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService projectTaskService;
        private readonly ILogger<ProjectTaskController> logger;

        public ProjectTaskController(IProjectTaskService projectTaskService, ILogger<ProjectTaskController> logger)
        {
            this.projectTaskService = projectTaskService;
            this.logger = logger;
        }

        [HttpGet("project/{projectId:int}/tasks")]
        public IActionResult GetTasksByProjectId([FromRoute] int projectId)
        {
            var tasks = this.projectTaskService.GetTasksByProjectId(projectId);
            return this.Ok(tasks);
        }
    }
}