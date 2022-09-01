using System.ComponentModel.DataAnnotations.Schema;

namespace OpenGMT
{
    public class Scene
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public long[] CreatureIds { get; set; }
        public long MapId { get; set; }

        public string StrongStart { get; set; }
        public string SecretsAndHints { get; set; }
        public string PhantasticLocations { get; set; }
        public string MagicItems { get; set; }
        public string[] Handouts { get; set; }
        public string[] Sounds { get; set; }
    }
}