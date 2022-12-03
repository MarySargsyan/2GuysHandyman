namespace _2GuysHandyman.models
{
    public class Orders
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public int ServiceId { get; set; }
        public int StatusId { get; set; }
        public Users User { get; set; }
        public Services Service { get; set; }
        public OrderStatuses Status { get; set; }
        public List<Files> Files { get; set; }

    }
}
