namespace GiterWebAPI.Services;

using DataAccessLayer.Interfaces;
using Entities;
using GiterWebAPI.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService
{

    private readonly IUserDAL _userDAL;

    public UserService(IUserDAL userDAL)
    {
        _userDAL = userDAL;
    }

    public async Task<Response> Delete(User user)
    {
        return await _userDAL.Delete(user);
    }

    public async Task<DataResponse<User>> GetAll()
    {
        return await _userDAL.GetAll();
    }

    public async Task<SingleResponse<User>> GetById(int id)
    {
        return await _userDAL.GetById(id);
    }

    public async Task<SingleResponse<User>> GetByUsername(string username)
    {
        return await _userDAL.GetByUsername(username);
    }

    public async Task<Response> Insert(User user)
    {

        return await _userDAL.Insert(user);
    }

    public async Task<Response> Update(User user)
    {
        return await _userDAL.Update(user);
    }
}