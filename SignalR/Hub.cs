using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace OpenGMT.SignalR
{
    public class GameHub : Hub
    {
        private readonly IWebHostEnvironment env;

        public GameHub(IWebHostEnvironment env)
        {
            this.env = env;
        }


        public async Task SendPlayers(string message)
        {
            Console.WriteLine("Got players");

/*
            string dataDir = env.ContentRootPath + "data";
            string dataFile = Path.Combine(dataDir, "session.json");
            if (Directory.Exists(dataDir) && System.IO.File.Exists(dataFile))
            {
                string data = System.IO.File.ReadAllText(dataFile);

                var session = JsonSerializer.Deserialize<Session>(data);
                if (session.Scene.Map.Markers == null)
                    session.Scene.Map.Markers = new List<MapMarker>();
                var newState = JsonSerializer.Deserialize<IList<MapMarker>>(message);

                if (newState != null)
                {
                    foreach (var n in newState)
                    {
                        var creature = session.Scene.Map.Markers.FirstOrDefault(c => c.CreatureId == n.CreatureId);
                        if (creature != null)
                        {
                            creature.X = n.X;
                            creature.Y = n.Y;
                            creature.Initiative = n.Initiative;
                            creature.Visible = n.Visible;
                            creature.VisionNormal = n.VisionNormal;
                            creature.VisionDim = n.VisionDim;
                        } else {
                            session.Scene.Map.Markers.Add(n);
                        }
                    }
                    File.WriteAllText(dataFile, JsonSerializer.Serialize(session));
                }
            }

*/
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

        public async Task SetHandout(string data)
        {
            Console.WriteLine("Set Handout");
            await Clients.All.SendAsync("setHandout", data);
        }

        public async Task SetFog(bool fogActive)
        {
            Console.WriteLine("Set Fog");
            await Clients.All.SendAsync("setFog", fogActive);
        }

        public async Task SetCombat(bool active)
        {
            Console.WriteLine("Set Combat");
            await Clients.All.SendAsync("setCombat", active);
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

        public async Task SessionInfo(string data)
        {
            Console.WriteLine("Session Info", data);
            await Clients.All.SendAsync("sessionInfo", data);
        }
    }
}