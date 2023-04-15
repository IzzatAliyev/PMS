using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Project;

namespace PMS.Web.Controllers
{
    [Route("board")]
    public class BoardController : Controller
    {
        private readonly IProjectService projectService;
        private readonly ILogger<BoardController> logger;

        public BoardController(IProjectService projectService, ILogger<BoardController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
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