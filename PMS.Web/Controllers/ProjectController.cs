using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.Project;
using PMS.Service.ViewModels.PTask;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("projects")]
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> logger;

        public ProjectController(ILogger<ProjectController> logger)
        {
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
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"projects/{id}/tasks");
                var projectTasks = await response.Content.ReadFromJsonAsync<IEnumerable<PTaskViewModel>>();
                if (projectTasks != null)
                {
                    return View(projectTasks);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync("projects");
                var projects = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectViewModel>>();
                if (projects != null)
                {
                    return View(projects);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }
    }
}