namespace GiterWebAPI.Controllers;

using AutoMapper;
using Entities;
using GifterWebApplication.Models.Authentication;
using GifterWebApplication.Models.Users;
using GiterWebAPI.Helpers;
using GiterWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }   

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
    [HttpPost]
    public async Task<IActionResult> Insert(UserInsertViewModel _user)
    {
        User user = _mapper.Map<User>(_user); 
        var response = await _userService.Insert(user);
        if (!response.HasSucess)
        {
            return BadRequest(response.Message);
        }
        return Created("Criado com sucesso",response);
    }
    
    //public async Task<IActionResult> Delete(UserDeleteViewModel _user)
    //{
    //    User user = _mapper.Map<User>(_user);
    //    return null;
    //}


}
