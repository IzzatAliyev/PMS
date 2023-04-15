using Microsoft.AspNetCore.Mvc;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.Project;
using PMS.Service.ViewModels.Skill;

namespace PMS.Web.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> IndexAsync([FromRoute] int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"employees/{id}");
                var employee = await response.Content.ReadFromJsonAsync<EmployeeViewModel>();
                if (employee != null)
                {
                    var response2 = await client.GetAsync($"employee-project/employee/{id}/projects");
                    var projects = await response2.Content.ReadFromJsonAsync<IEnumerable<ProjectViewModel>>();
                    ViewBag.Projects = projects; 
                    var response3 = await client.GetAsync($"employee-skill/employee/{id}/skills");
                    var skills = await response3.Content.ReadFromJsonAsync<IEnumerable<SkillViewModel>>();
                    ViewBag.Skills = skills;
                    return View(employee);
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
                var response = await client.GetAsync("employees");
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
                if (employees != null)
                {
                    return View(employees);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }
    }
}