using System.Text.Json.Serialization;

namespace OpenGMT
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IList<Campaign> Campaigns { get; set; }
    }
}