using AutoMapper;
using Entities;
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
    private readonly IMapper _mapper;
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users.Item);
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
        return Created("Criado com sucesso", response);
    }

    //[Authorize(Roles = "Manager")]
    //[HttpDelete]
    //public async Task<IActionResult> Delete(UserDeleteViewModel _user)
    //{
    //    User user = _mapper.Map<User>(_user);
    //    var response = await _userService.Delete(user);
    //    if (!response.HasSucess)
    //    {
    //        return BadRequest(response.Message);
    //    }
    //    return Ok(response.Message);
    //}

    //[HttpPut]
    //[Authorize(Roles = "Manager")]
    //public async Task<IActionResult> Update(UserUpdateViewModel _user)
    //{
    //    User user = _mapper.Map<User>(_user);

    //    var response = await _userService.Update(user);
    //    if (!response.HasSucess)
    //    {
    //        return BadRequest(response.Message);
    //    }
    //    return Ok(response.Message);
    //}
}
