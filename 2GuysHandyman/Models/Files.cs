namespace _2GuysHandyman.models
{
    public class Files
    {
        public int Id { get; set; }
        public byte[] FileLink { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }

    }
}
