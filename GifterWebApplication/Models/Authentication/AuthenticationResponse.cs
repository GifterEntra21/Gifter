using Entities;
using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Authentication
{
    public class AuthenticationResponse 
    {
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }


    }
}
