using Microsoft.AspNetCore.SignalR;

namespace Farma
{
    public class MedicamentosHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
