using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PMS.Infrastructure.Entities;
using PMS.Service.Services;
using PMS.Service.ViewModels.Employee;
using PMS.Web.Hubs;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("chat")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> chatHubContext;
        private UserManager<User> userManager;
        private readonly ChatService chatService;

        public ChatController(IHubContext<ChatHub> chatHubContext, UserManager<User> userManager, ChatService chatService)
        {
            this.chatHubContext = chatHubContext;
            this.userManager = userManager;
            this.chatService = chatService;
        }

        [HttpGet("employee/{id:int}")]
        public async Task<IActionResult> IndexAsync([FromRoute] int id)
        {
            var currentUser = this.userManager.GetUserName(User);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync($"employees/id?email={currentUser}");
                var employeeId = await response.Content.ReadFromJsonAsync<int>();
                if (employeeId != 0)
                {
                    return View(employeeId);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var currentUser = this.userManager.GetUserName(User);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync("employees");
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
                if (employees != null)
                {
                    var response2 = await client.GetAsync($"employees/id?email={currentUser}");
                    var employeeId = await response2.Content.ReadFromJsonAsync<int>();
                    ViewData["employeeId"] = employeeId;
                    return View(employees);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            var user = userManager.GetUserName(User);
            await this.chatHubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return Ok();
        }
    }

}