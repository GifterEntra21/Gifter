using Entities;
using Shared.Responses;

namespace GiterWebAPI.Interfaces
{
    public interface IUserService
    {

        Task<Response> Insert(User user);
        Task<Response> Update(User user);
        Task<Response> Delete(User user);
        Task<DataResponse<User>> GetAll();
        Task<SingleResponse<User>> GetById(int id);
        Task<SingleResponse<User>> GetByUsername(User model);
    }
}
