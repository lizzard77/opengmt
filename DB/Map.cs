using System.Text.Json.Serialization;

namespace OpenGMT
{
    public class Map
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string ImageUrlDM { get; set; }
        public long ImageWidth { get; set; }
        public long ImageHeight { get; set; }

        public decimal WidthInSquares { get; set; }
        public decimal HeightInSquares { get; set; }
        public decimal FootPerSquare { get; set; }
        public float GridOpacity { get; set; }

        public IList<MapMarker> Markers { get; set; } = new List<MapMarker>();

        [JsonIgnore]
        public IList<Scene> Scenes { get; set; }
    }
}