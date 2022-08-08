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

        public async Task SetCurrentPlayer(string data)
        {
            Console.WriteLine("Set Current Player");
            await Clients.All.SendAsync("setCurrentPlayer", data);
        }

        public async Task LoadScene(long id)
        {
            Console.WriteLine("Load Scene");
            await Clients.All.SendAsync("loadScene", id);
        }

        public async Task CenterMap(float x, float y)
        {
            Console.WriteLine("Center Map", x, y);
            await Clients.All.SendAsync("centerMap", x, y);
        }
    }
}