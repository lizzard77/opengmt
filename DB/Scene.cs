using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenGMT
{
    public class Scene
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Creature> Creatures { get; set; } = new List<Creature>();
        public IList<MapMarker> Markers { get; set; } = new List<MapMarker>();
        public Map Map { get; set; }
        public IList<Asset> Assets { get; set; } = new List<Asset>();
        
        public string StrongStart { get; set; }
        public string SecretsAndHints { get; set; }
        public string PhantasticLocations { get; set; }
        public string MagicItems { get; set; }
        public string ScenesAndEncounters { get; set; }

    }
}