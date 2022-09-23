namespace GifterWebApplication.Models.Users
{
    public class UserSelectViewModel
    { 
        public int Id { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public string AcessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
