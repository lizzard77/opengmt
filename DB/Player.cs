using System.Text.Json.Serialization;

namespace OpenGMT
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IList<Session> Sessions { get; set; }
    }
}