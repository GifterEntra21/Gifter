using Entities;
using GiterWebAPI.Models;
using Shared.Resposes;

namespace GiterWebAPI.Interfaces
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        IEnumerable<APIUser> GetAll();
        APIUser GetById(int id);

    }
}
