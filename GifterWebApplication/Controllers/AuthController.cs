using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
            var _loginModel = _mapper.Map<User>(loginModel);

            var user = await _userService.Login(_loginModel);
            if (user is null)
                return Unauthorized();

            //aqui tem coisa dando errado 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "string"),
                new Claim(ClaimTypes.Role, "Manager")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.Item.AcessToken = accessToken;
            user.Item.RefreshToken = refreshToken;
            user.Item.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            //_userContext.SaveChanges();
            //substituir por update?
            await _userService.Update(user.Item);

            return Ok(new AuthenticationResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
