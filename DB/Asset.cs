namespace OpenGMT
{
    public class Asset
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public AssetType Type { get; set; }
        
        public bool? Loop { get; set; }
        public int? OffsetSeconds { get; set; }
        public float? Volume { get; set; }
        public string Uri { get; set; }
    }

    public enum AssetType
    {
        GenericFile = 0,
        Image = 1,
        Audio = 2,
        Video = 3
    }
}