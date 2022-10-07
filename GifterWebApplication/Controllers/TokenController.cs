
using BusinessLogicalLayer.Interfaces;
using Entities;
using GifterWebApplication.Models.Authentication;
using JwtAuthentication.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

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

        [HttpPost("/refresh")]
        [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var _username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = _userService.GetByUsername(new APIUser() { Username = _username });

            if (user == null || user.Result.Item.RefreshToken != refreshToken || user.Result.Item.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }

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

        [HttpPost("/revoke")]
        [Authorize]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Revoke()
        {
            var _username = User.Identity.Name;

            SingleResponse<APIUser> user = await _userService.GetByUsername(new APIUser { Username = _username });
            if (user == null)
            {
                return BadRequest();
            }

            user.Item.RefreshToken = null;
            user.Item.RefreshTokenExpiryTime = null;

            await _userService.Update(user.Item);
            return NoContent();
        }
    }
}
