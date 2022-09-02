using Entities;
using GiterWebAPI.Models;
using Shared.Resposes;

namespace GiterWebAPI.Interfaces
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);

    }
}
