namespace GiterWebAPI.Controllers;

using GifterWebApplication.Models.Authentication;
using GiterWebAPI.Helpers;
using GiterWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService, IOptions<AppSettings> settings)
    {
        _userService = userService;
    }   

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
}
