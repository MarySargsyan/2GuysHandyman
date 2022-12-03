namespace _2GuysHandyman.models
{
    public class Users
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Mobile { get; set; }
        public string Adress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
