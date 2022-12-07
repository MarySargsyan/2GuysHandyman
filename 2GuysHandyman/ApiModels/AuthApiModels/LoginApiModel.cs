using System.ComponentModel.DataAnnotations;

namespace _2GuysHandyman.ApiModels.AuthApiModels
{
    public class LoginApiModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
