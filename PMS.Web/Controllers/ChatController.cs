using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PMS.Infrastructure.Entities;
using PMS.Web.Hubs;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("chat")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> chatHubContext;
        private UserManager<User> userManager;

        public ChatController(IHubContext<ChatHub> chatHubContext, UserManager<User> userManager)
        {
            this.chatHubContext = chatHubContext;
            this.userManager = userManager;
        }
        [HttpGet("user/1")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllUsers()
        {
            return View();
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