using Microsoft.AspNetCore.SignalR;

namespace OpenGMT.SignalR
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string message)
        {
            Console.WriteLine("111");
            await Clients.All.SendAsync("send", message);
        }
    }
}