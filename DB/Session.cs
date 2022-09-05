using System.Text.Json.Serialization;

namespace OpenGMT
{
    public class Session
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public long SceneId { get; set; }
        public Scene Scene { get; set; }
    }
}