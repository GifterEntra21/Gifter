
using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Entities;
using GifterWebApplication.Models.Authentication;
using JwtAuthentication.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GifterWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBLL _userService;     
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IUserBLL userService,  ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;

            _tokenService = tokenService;

            _mapper = mapper;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
            APIUser _loginModel = _mapper.Map<APIUser>(loginModel);

            var user = await _userService.Login(_loginModel);
            if (user.Item is null)
            {
                return Unauthorized();
            }
            //fazer a verificação de login com o cosmos


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Item.Username),
                new Claim(ClaimTypes.Role, "Manager")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.Item.RefreshToken = refreshToken;
            user.Item.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userService.Update(user.Item);
            //substituir por update?

            return Ok(new AuthenticationResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
