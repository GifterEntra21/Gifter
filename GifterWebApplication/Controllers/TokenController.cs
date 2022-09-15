using AutoMapper;
using Entities;
using GifterWebApplication.Models.Authentication;
using GiterWebAPI.Interfaces;
using JwtAuthentication.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GifterWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        //imapper n precisa
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public TokenController(IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel) 
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = _userService.GetByUsername(new User {Username = username });

            if (user == null || user.Result.Item.RefreshToken != refreshToken || user.Result.Item.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.Result.Item.RefreshToken = newRefreshToken;
            _userService.Update(user.Result.Item);

            return Ok(new AuthenticationResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;

            var user = await _userService.GetByUsername(new User { Username = username });
            if (user == null) return BadRequest();

            user.Item.RefreshToken = null;
            user.Item.RefreshTokenExpiryTime = null;

            _userService.Update(user.Item);

            return NoContent();
        }
    }
}
