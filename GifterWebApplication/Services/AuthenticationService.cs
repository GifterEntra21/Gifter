using Entities;
using GifterWebApplication.Interfaces;
using GifterWebApplication.Models.Authentication;
using GiterWebAPI.Helpers;
using GiterWebAPI.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GifterWebApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
       
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;

        public AuthenticationService(IOptions<AppSettings> appSettings, IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }
        public AuthenticationResponse Authenticate(AuthenticationRequest model)
        {
            var user = _userService.GetByUsername(model.Username);
            //x => x.Username == model.Username && x.Password == model.Password

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticationResponse(user, token);
            return null;
        }
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
