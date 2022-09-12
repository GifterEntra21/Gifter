using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Users
{
    public class UserInsertViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
