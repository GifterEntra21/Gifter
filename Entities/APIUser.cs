using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{

    public class APIUser
    {
        public APIUser(int iD, string username, string email, bool isActive, string? refreshToken, DateTime? refreshTokenExpiryTime, string password)
        {
            ID = iD;
            Username = username;
            Email = email;
            IsActive = isActive;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
            Password = password;
        }
        public APIUser()
        {
        }

        public int ID { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }


        [JsonIgnore]
        public string Password { get; set; }
    }

}
