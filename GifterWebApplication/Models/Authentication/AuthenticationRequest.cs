using System.ComponentModel.DataAnnotations;
namespace GifterWebApplication.Models.Authentication
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
