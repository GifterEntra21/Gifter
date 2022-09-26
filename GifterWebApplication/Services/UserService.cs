namespace GiterWebAPI.Services;

using Entities;
using GifterWebApplication.Models.Authentication;
using GiterWebAPI.Helpers;
using GiterWebAPI.Interfaces;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<APIUser> _users = new List<APIUser>
    {
        new APIUser()
    };

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public AuthenticationResponse Authenticate(AuthenticationRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticationResponse();
    }

    public async Task<DataResponse<APIUser>> GetAll()
    {
        DataResponse<APIUser> users = new("", true, _users, new Exception());
        return users ;
    }

    public async Task<SingleResponse<APIUser>> GetById(string id)
    {
        return new SingleResponse<APIUser>("", true, new APIUser(), new Exception()); 
    }


    // helper methods

    private string generateJwtToken(APIUser user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}