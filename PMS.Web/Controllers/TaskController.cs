using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.PTask;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("tasks")]
    public class TaskController : Controller
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> IndexAsync([FromRoute] int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"tasks/{id}");
                var task = await response.Content.ReadFromJsonAsync<PTaskViewModel>();
                if (task != null)
                {
                    var projectId = task.ProjectId;
                    var response2 = await client.GetAsync($"projects/{projectId}/name");
                    var projectName = await response2.Content.ReadAsStringAsync();
                    ViewBag.ProjectName = projectName;

                    var response3 = await client.GetAsync("employees");
                    var employees = await response3.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
                    ViewBag.Employees = employees;
                    return View(task);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(PTaskViewModel createModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var task = new PTaskViewModel()
                {
                    Name = createModel.Name,
                    Description = createModel.Description,
                    TaskType = createModel.TaskType,
                    AssignedToId = createModel.AssignedToId,
                    AssignedFromId = createModel.AssignedFromId,
                    Status = createModel.Status,
                    ProjectId = createModel.ProjectId
                };

                var content = new StringContent(JsonSerializer.Serialize(task));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await client.PostAsync("tasks", content);
                var responseMessage = await response.Content.ReadAsStringAsync();

                return RedirectToAction("GetTasksByProjectId", "Project", new { id = task.ProjectId });
            }

        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateAsync(int id, PTaskViewModel updatedModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"tasks/{id}");
                var taskDb = await response.Content.ReadFromJsonAsync<PTaskViewModel>();
                if (taskDb != null)
                {
                    var task = new PTaskViewModel()
                    {
                        Name = updatedModel.Name,
                        Description = updatedModel.Description,
                        TaskType = updatedModel.TaskType,
                        AssignedToId = updatedModel.AssignedToId,
                        AssignedFromId = updatedModel.AssignedFromId,
                        Status = updatedModel.Status,
                        ProjectId = updatedModel.ProjectId
                    };

                    ViewData["projectId"] = taskDb.ProjectId;

                    var content = new StringContent(JsonSerializer.Serialize(task));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response2 = await client.PatchAsync($"tasks/{id}", content);
                    var responseMessage = await response2.Content.ReadAsStringAsync();

                    return RedirectToAction("GetTasksByProjectId", "Project", new { id = taskDb.ProjectId });
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }
    }
}