using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.Project;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync("employees");
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
                var response2 = await client.GetAsync("projects");
                var projects = await response2.Content.ReadFromJsonAsync<IEnumerable<ProjectViewModel>>();
                if (employees != null && projects != null)
                {
                    ViewBag.Employees = employees;
                    ViewBag.Projects = projects;
                    return View();
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"employee-project/search?query={query}");
                var matchingData = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeProjectSearchViewModel>>();
                if (matchingData != null)
                {
                    return new JsonResult(matchingData);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }
    }
}