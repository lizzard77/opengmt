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

        public async Task MovePlayer(string data)
        {
            Console.WriteLine("Move Player");
            await Clients.All.SendAsync("move", data);
        }

        public async Task LoadScene(long id)
        {
            Console.WriteLine("Load Scene");
            await Clients.All.SendAsync("loadScene", id);
        }

        public async Task MoveMap(long left, long top)
        {
            Console.WriteLine("Move Map");
            await Clients.All.SendAsync("moveMap", left, top);
        }
    }
}