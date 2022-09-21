using Entities;
using System.ComponentModel.DataAnnotations;

namespace GiterWebAPI.Models
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(string? refreshToken, string? token)
        {
            RefreshToken = refreshToken;
            Token = token;
        }

        public string? RefreshToken { get; set; }
        public string? Token { get; set; }

    }
}
