using Microsoft.AspNetCore.SignalR;

namespace OpenGMT.SignalR
{
    public class GameHub : Hub
    {
        public async Task SendPlayers(string message)
        {
            Console.WriteLine("Got players");
            await Clients.All.SendAsync("players", message);
        }
    }
}