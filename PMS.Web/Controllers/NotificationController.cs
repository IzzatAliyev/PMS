using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PMS.Infrastructure.Entities;
using PMS.Web.Hubs;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("notification")]
    public class NotificationController : Controller
    {
        private readonly IHubContext<NotificationHub> notificationHubContext;
        private UserManager<User> userManager;

        public NotificationController(IHubContext<NotificationHub> notificationHubContext, UserManager<User> userManager)
        {
            this.notificationHubContext = notificationHubContext;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(string message)
        {
            await this.notificationHubContext.Clients.All.SendAsync("ReceiveNotification", message);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageToUser(string userId, string message)
        {
            await this.notificationHubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
            return Ok();
        }
    }
}