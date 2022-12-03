namespace _2GuysHandyman.ApiModels
{
    public class OrdersApiModel
    {
        public string? Description { get; set; }

       //public Guid UserId { get; set; }
        public int ServiceId { get; set; }

       //public int StatusId { get; set; } statusId = 1 (created)
        public List<FilesApiModel> Files { get; set; }
    }
}
