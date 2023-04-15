using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.PTask;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly ILogger<TaskController> logger;

        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            this.taskService = taskService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] PTaskViewModel task)
        {
            await this.taskService.CreateTask(task);
            return this.Created(nameof(CreateTask), "Successfully created!");
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateTaskById([FromRoute] int id, [FromBody] PTaskViewModel task)
        {
            try
            {
                await this.taskService.UpdateTaskById(id, task);
                return this.Created(nameof(UpdateTaskById), "Successfully updated!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTaskById([FromRoute] int id)
        {
            try
            {
                await this.taskService.DeleteTaskById(id);
                return this.Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById([FromRoute] int id)
        {
            try
            {
                var task = await this.taskService.GetTaskById(id);
                return this.Ok(task);
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
                var tasks = this.taskService.GetAllTasks();
                return this.Ok(tasks);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }
    }
}