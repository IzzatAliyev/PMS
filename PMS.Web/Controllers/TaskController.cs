using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                    return View(task);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }
    }
}