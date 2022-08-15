using System.ComponentModel.DataAnnotations.Schema;

namespace OpenGMT
{
    public class Scene
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Creature> Creatures { get; set; }
        public IList<CreatureState> CreatureStates { get; set; }
        public Map Map { get; set; }
    }

    public class CreatureState
    {
        public long CreatureStateId { get; set; }

        public Creature Creature  { get; set; }
        public Map Map { get; set; }

        public long X { get; set; }
        public long Y { get; set; }
        public long Initiative { get; set; }
        public bool Visible { get; set; }
        public long VisionNormal { get; set; }
        public long VisionDim { get; set; }
    }

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
    }
}