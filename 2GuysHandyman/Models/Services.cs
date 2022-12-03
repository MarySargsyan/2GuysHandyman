namespace _2GuysHandyman.models
{
    public class Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
