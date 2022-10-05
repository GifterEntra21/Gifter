
using Entities;
using Shared.Responses;
using GifterWebApplication.Models.Users;
using GiterWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GiterWebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users.ItemList);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(UserInsertViewModel _user)
    {
        //User user = _mapper.Map<User>(_user);
        //var response = await _userService.Insert(user);
        // response so pra compilar
        Response response = new("", true, new Exception());
        
        if (!response.HasSuccess)
        {
            return BadRequest(response.Message);
        }
        return Created("Criado com sucesso", response);
    }
}
