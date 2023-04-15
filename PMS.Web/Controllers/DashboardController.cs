using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.Project;

namespace PMS.Web.Controllers
{
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IProjectService projectService;
        private readonly IMemoryCache cache;
        private readonly ILogger<DashboardController> logger;

        public DashboardController(IEmployeeService employeeService, IProjectService projectService, IMemoryCache cache, ILogger<DashboardController> logger)
        {
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.cache = cache;
            this.logger = logger;
        }

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
        public IActionResult Search([FromQuery] string query)
        {
            var queryLowerCase = query.ToLower();

            if (cache.TryGetValue(queryLowerCase, out List<EmployeeProjectSearchViewModel> cachedResults))
            {
                return new JsonResult(cachedResults);
            }

            var projects = this.projectService.GetAllProjects();
            var employees = this.employeeService.GetAllEmployees();

            var matchingProjects = projects
                .Where(p => p.Name.ToLower().Contains(queryLowerCase) || p.Description.ToLower().Contains(queryLowerCase))
                .Select(p => new EmployeeProjectSearchViewModel
                {
                    Name = p.Name,
                    Url = $"http://localhost:5047/projects/{p.Id}",
                    Type = "project"
                })
                .ToList();

            var matchingEmployees = employees
                .Where(e => e.Name.ToLower().Contains(queryLowerCase))
                .Select(e => new EmployeeProjectSearchViewModel
                {
                    Name = e.Name,
                    Url = $"http://localhost:5047/employees/{e.Id}",
                    Type = "employee"
                })
                .ToList();

            var suitableData = matchingProjects.Concat(matchingEmployees).ToList();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            cache.Set(queryLowerCase, suitableData, cacheEntryOptions);

            return new JsonResult(suitableData);
        }
    }
}