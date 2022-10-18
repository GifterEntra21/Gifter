using BusinessLogicalLayer.Interfaces;
using Entities;
using GifterWebApplication.Models.Authentication;
using JwtAuthentication.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace GifterWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
        public IActionResult Refresh([FromBody] TokenApiModel tokenApiModel)
        {
            try
            {
                if (tokenApiModel is null)
                {
                    return BadRequest("Invalid client request");
                }

                string accessToken = tokenApiModel.AccessToken;
                string refreshToken = tokenApiModel.RefreshToken;

                var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken).Item;
                var _username = principal.Identity.Name; //this is mapped to the Name claim by default

                var user = _userService.GetByUsername(new APIUser() { Username = _username });

                if (user == null || user.Result.Item.RefreshToken != refreshToken || user.Result.Item.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return BadRequest("Invalid client request");
                }

                var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims).Item;
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                user.Result.Item.RefreshToken = newRefreshToken;

                _userService.Update(user.Result.Item);

                return Ok(new AuthenticationResponse()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
            
        }

        [HttpPost]
        [Route("revoke")]
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

            Response resp = await _userService.Update(user.Item);
            if (!resp.HasSuccess)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
