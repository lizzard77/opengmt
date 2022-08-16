namespace OpenGMT
{
    public class Session
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public long SceneId { get; set; }
        //public Scene CurrentScene { get; set; }
        public IList<CreatureState> CreatureStates { get; set; } = new List<CreatureState>();
    }
}