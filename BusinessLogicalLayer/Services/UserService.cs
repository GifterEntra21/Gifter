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
        try
        {
            return await _userDAL.Delete(user);

        }
        catch (Exception ex)
        {

            return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
        }
    }

    public async Task<DataResponse<User>> GetAll()
    {
        return await _userDAL.GetAll();
    }

    public async Task<SingleResponse<User>> GetById(int id)
    {
        try
        {
            return await _userDAL.GetById(id);

        }
        catch (Exception ex)
        {

            return ResponseFactory.CreateInstance().CreateSingleNotFoundIdResponse<User>(null);
        }
    }

    public async Task<SingleResponse<User>> Login(User username)
    {
        try
        {
            return await _userDAL.Login(username);
        }
        catch (Exception ex)
        {

            return ResponseFactory.CreateInstance().CreateSingleNotFoundIdResponse<User>(null);
        }
    }

    public async Task<Response> Insert(User user)
    {
        try
        {
            return await _userDAL.Insert(user);

        }
        catch (Exception ex)
        {

            return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
        }
    }

    public async Task<Response> Update(User user)
    {
        try
        {
            return await _userDAL.Update(user);

        }
        catch (Exception ex)
        {

            return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
        }
    }

    public async Task<SingleResponse<User>> GetByUsername(User model)
    {
        try
        {
            return await _userDAL.GetByUsername(model);

        }
        catch (Exception ex)
        {

            return ResponseFactory.CreateInstance().CreateSingleNotFoundIdResponse<User>(null);
        }
    }
}