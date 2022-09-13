using System.ComponentModel.DataAnnotations;

namespace GifterWebApplication.Models.Users
{
    public class UserInsertViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public bool IsActive { get { return true; } }
        [Required]
        public string Password { get; set; }

    }
}
