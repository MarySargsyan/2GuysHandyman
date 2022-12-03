using System.ComponentModel.DataAnnotations;

namespace _2GuysHandyman.ApiModels
{
    public class UsersApiModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Mobile { get; set; }
        public string Adress { get; set; }
        public string? Password { get; set; }
    }
}
