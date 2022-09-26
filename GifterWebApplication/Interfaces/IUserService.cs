using Entities;
using GifterWebApplication.Models.Authentication;
using Shared.Responses;

namespace GiterWebAPI.Interfaces
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        Task<DataResponse<APIUser>> GetAll();
        Task<SingleResponse<APIUser>> GetById(string id);

    }
}
