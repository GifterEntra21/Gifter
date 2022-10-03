
using Entities;
using GifterWebApplication.Models.Authentication;
using GiterWebAPI.Interfaces;
using JwtAuthentication.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GifterWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService,  ITokenService tokenService)
        {
            _userService = userService;

            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
           /* var _loginModel = _mapper.Map<APIUser>(loginModel);

            var user = await _userService.Login(_loginModel);
            if (user is null)

            */

            //fazer a verificação de login com o cosmos
                return Unauthorized();

             
            var claims = new List<Claim>
            {

               // new Claim(ClaimTypes.Name, user.Item.Username),
                new Claim(ClaimTypes.Role, "Manager")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            //user.Item.AcessToken = accessToken;
            
            //user.Item.RefreshToken = refreshToken;
            //user.Item.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            //_userContext.SaveChanges();
            //substituir por update?
            
            //await _userService.Update(user.Item);

            return Ok(new AuthenticationResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
