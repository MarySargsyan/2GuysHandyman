using System.ComponentModel.DataAnnotations;

namespace _2GuysHandyman.ApiModels
{
    public class LoginApiModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
