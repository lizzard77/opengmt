using System.Text.Json.Serialization;

namespace OpenGMT
{
    public class Campaign
    {
        public long Id { get; set; }
        public string Name { get; set; }
    
        public long? CurrentSceneId { get; set; }
        public Scene CurrentScene { get; set; }

        public IList<Player> Players { get; set; }

        public long DMPlayerId { get; set; }
    }
}