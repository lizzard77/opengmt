namespace OpenGMT
{
    public class CreatureState
    {
        public long CreatureId { get; set; }
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