using System.Text.Json.Serialization;

namespace OpenGMT
{
    public class MapMarker
    {
        public long Id { get; set; }

        public long CreatureId { get; set; }
        [JsonIgnore]
        public Creature Creature { get; set; }
        public long MapId { get; set; }
        [JsonIgnore]
        public Map Map { get; set; }
        public long SceneId { get; set; }
        [JsonIgnore]
        public Scene Scene { get; set; }

        public decimal X { get; set; }
        public decimal Y { get; set; }
        public long Initiative { get; set; }
        public bool Visible { get; set; }
        public bool Light { get; set; }
        public long VisionNormal { get; set; }
        public long VisionDim { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public decimal Reach { get; set; }
    }
}