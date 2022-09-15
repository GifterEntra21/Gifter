using GifterWebApplication.Interfaces;
using GifterWebApplication.Models.Authentication;
using GiterWebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GifterWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //controlol
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService, IOptions<AppSettings>settings)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequest model)
        {
            var response = _authenticationService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response.Result);
        }
    }
}
