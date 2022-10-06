﻿
using BusinessLogicalLayer.Interfaces;
using Entities;
using GifterWebApplication.Models.Authentication;
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
        private readonly IUserBLL _userService;

        private readonly ITokenService _tokenService;

        public TokenController(IUserBLL userService, ITokenService tokenService)
        {
            _userService = userService;
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
            var _username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = _userService.GetByUsername(new APIUser(){ Username = _username});

            if (user == null || user.Result.Item.RefreshToken != refreshToken || user.Result.Item.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.Result.Item.RefreshToken = newRefreshToken;

            _userService.Update(user.Result.Item);

            return Ok(new AuthenticationResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var _username = User.Identity.Name;

            var user = await _userService.GetByUsername(new APIUser { Username = _username});
            if (user == null) return BadRequest();

            user.Item.RefreshToken = null;
            user.Item.RefreshTokenExpiryTime = null;


            return NoContent();
        }
    }
}
