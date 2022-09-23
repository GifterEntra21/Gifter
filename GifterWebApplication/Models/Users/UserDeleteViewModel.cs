using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Users
{
    public class UserDeleteViewModel
    {
        
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }  
    }
}
