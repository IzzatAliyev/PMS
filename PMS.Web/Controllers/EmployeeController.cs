using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeSkill;
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
                    ViewBag.SkillsName = skills.Select(x => x.Name).ToList();
                    var response4 = await client.GetAsync("skills");
                    var allSkills = await response4.Content.ReadFromJsonAsync<IEnumerable<SkillViewModel>>();
                    ViewBag.AllSkills = allSkills;
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

        [HttpPost("update")]
        public async Task<ActionResult> UpdateAsync(int id, EmployeeWithSkillsViewModel updatedModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"employees/{id}");
                var employeeDb = await response.Content.ReadFromJsonAsync<EmployeeViewModel>();
                if (employeeDb != null)
                {
                    var employee = new EmployeeViewModel()
                    {
                        UserName = updatedModel.UserName,
                        FirstName = updatedModel.FirstName,
                        LastName = updatedModel.LastName,
                        Position = updatedModel.Position,
                        Email = updatedModel.Email,
                        Description = updatedModel.Description,
                        PhoneNumber = updatedModel.PhoneNumber
                    };

                    var skills = updatedModel.Skills.Split(",");
                    var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(employee));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response2 = await client.PatchAsync($"employees/{id}", content);
                    var responseMessage = await response2.Content.ReadAsStringAsync();

                    foreach(var skill in skills){
                        var response3 = await client.GetAsync($"skills/name?name={skill}");
                        var skillDb = await response3.Content.ReadFromJsonAsync<SkillViewModel>();
                        var skillId = skillDb!.Id;

                        var employeeSkill = new EmployeeSkillViewModel()
                        {
                            EmployeeId = id,
                            SkillId = skillId
                        };
                        var skillContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(employeeSkill));
                        skillContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response4 = await client.PostAsync("employee-skill", skillContent);
                        var response5 = await client.DeleteAsync("employee-skill/duplicate");
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