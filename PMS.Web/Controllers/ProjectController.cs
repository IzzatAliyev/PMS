using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.Project;
using PMS.Service.ViewModels.PTask;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("projects")]
    public class ProjectController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(UserManager<User> userManager, ILogger<ProjectController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> IndexAsync([FromRoute] int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"projects/{id}");
                var project = await response.Content.ReadFromJsonAsync<ProjectViewModel>();
                if (project != null)
                {
                    var response2 = await client.GetAsync($"employee-project/project/{id}/employees");
                    var employees = await response2.Content.ReadFromJsonAsync<IEnumerable<EmployeeWithRoleViewModel>>();
                    ViewBag.Employees = employees;
                    ViewBag.EmployeesName = employees.Select(x => x.UserName).ToList();;

                    var response3 = await client.GetAsync("employees");
                    var allEmployees = await response3.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
                    ViewBag.AllEmployees = allEmployees;

                    return View(project);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }

        [HttpGet("{id:int}/tasks")]
        public async Task<IActionResult> GetTasksByProjectIdAsync([FromRoute] int id)
        {
            using (var client = new HttpClient())
            {
                var user = await this.userManager.GetUserAsync(User);
                var email = user.Email;
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"projects/{id}/tasks");
                var projectTasks = await response.Content.ReadFromJsonAsync<IEnumerable<PTaskWithAssignedNamesViewModel>>();
                if (projectTasks != null)
                {
                    var response2 = await client.GetAsync("employees");
                    var employees = await response2.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
                    ViewData["employees"] = employees;

                    var response3 = await client.GetAsync($"employees/employee?email={email}");
                    var employee = await response3.Content.ReadFromJsonAsync<EmployeeViewModel>();
                    ViewData["employee"] = employee;
                    ViewData["projectId"] = id.ToString();

                    return View(projectTasks);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] IEnumerable<ProjectStatus> status)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync("projects");
                var projects = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectViewModel>>();
                if (projects != null)
                {
                    if (status != null && status.Any())
                    {
                        projects = projects.Where(p => status.Contains(p.Status));
                    }
                    return View(projects);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateAsync(int id, ProjectWithEmployeesViewModel updatedModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"projects/{id}");
                var projectDb = await response.Content.ReadFromJsonAsync<ProjectViewModel>();
                if (projectDb != null)
                {
                    var project = new ProjectViewModel()
                    {
                        Name = updatedModel.Name,
                        Description = updatedModel.Description,
                        Status = updatedModel.Status
                    };

                    var employees = updatedModel.Employees != null ? updatedModel.Employees.Split(",") : Array.Empty<string>();
                    var content = new StringContent(JsonSerializer.Serialize(project));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response2 = await client.PatchAsync($"projects/{id}", content);
                    var responseMessage = await response2.Content.ReadAsStringAsync();

                    foreach(var employee in employees){
                        var response3 = await client.GetAsync($"employees/name?name={employee}");
                        var employeeDb = await response3.Content.ReadFromJsonAsync<EmployeeViewModel>();
                        var employeeId = employeeDb!.Id;

                        var employeeProject = new EmployeeProjectViewModel()
                        {
                            EmployeeId = employeeId,
                            ProjectId = id,
                            Task = EmployeeTask.Planning
                        };
                        var projectContent = new StringContent(JsonSerializer.Serialize(employeeProject));
                        projectContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response4 = await client.PostAsync("employee-project", projectContent);
                        var response5 = await client.DeleteAsync("employee-project/duplicate");
                    }
                    return RedirectToAction("Index", new { id = id} );
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }
    }
}