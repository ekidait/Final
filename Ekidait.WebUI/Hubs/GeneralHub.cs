using Microsoft.AspNetCore.SignalR;

namespace Ekidait.WebUI.Hubs
{
    public class GeneralHub : Hub
    {
        public async Task NotifyCategoryChange(string message, int categoryCount)
        {
            await Clients.All.SendAsync("ReceiveCategoryNotification", message, categoryCount);
        }
    }
}