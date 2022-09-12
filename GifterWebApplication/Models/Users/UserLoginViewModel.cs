using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Users
{
    public class UserLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
