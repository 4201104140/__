using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Api.Hubs
{
    public class NotificationHub : Hub
    {
        public Task SendMessage(string messgae)
        {
            return Clients.All.SendAsync("ReceiveMessage", messgae);
        }
    }
}
