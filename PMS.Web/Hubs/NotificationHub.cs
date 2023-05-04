using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PMS.Web.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveNotification", message, Context.User.Identity.Name);
        }
    }
}