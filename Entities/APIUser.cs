using Entities.Interfaces;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{

    public class APIUser : ICosmosDbItem
    {

        public APIUser()
        {
            this.id = Guid.NewGuid().ToString();
        }

        public APIUser(string username, string email, bool isActive, string? refreshToken, DateTime? refreshTokenExpiryTime, string password, RolesEnum role, string id)
        {
            Username = username;
            Email = email;
            IsActive = isActive;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
            Password = password;
            Role = role;
            this.id = id;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }


        [JsonIgnore]
        public string Password { get; set; }

        public RolesEnum Role { get; set; }

        public string id { get; set; }

        public string PartitionKey { get { return id; } }

    }

}
