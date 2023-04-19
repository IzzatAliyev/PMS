using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.Project;
using PMS.Service.ViewModels.Skill;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("employees")]
    public class EmployeeController : Controller
    {
        private UserManager<User> userManager;
        private IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(UserManager<User> userManager, IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.userManager = userManager;
            this.employeeService = employeeService;
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

        [HttpPost]
        public async Task<IActionResult> UploadMedia(IFormFile mediaFile, int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5275/api2/");
                var formData = new MultipartFormDataContent();
                var stringContent = new StringContent(employeeId.ToString());
                var fileContent = new StreamContent(mediaFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                formData.Add(fileContent, "mediaFile", mediaFile.FileName);
                formData.Add(stringContent, "employeeId");
                var response = await client.PostAsync("mediums", formData);
                var res = await response.Content.ReadAsStringAsync();
                await this.employeeService.SetProfilePictureByEmployeeId(employeeId, res);
                return response.IsSuccessStatusCode ? RedirectToAction("Index", new { id = employeeId} ) : RedirectToAction("SomethingWentWrong", "Home");
            }
        }
    }
}