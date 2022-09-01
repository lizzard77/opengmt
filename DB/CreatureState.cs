namespace OpenGMT
{
    public class CreatureState
    {
        public long CreatureId { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public long Initiative { get; set; }
        public bool Visible { get; set; }
        public bool Light { get; set; }
        public long VisionNormal { get; set; }
        public long VisionDim { get; set; }
    }
}