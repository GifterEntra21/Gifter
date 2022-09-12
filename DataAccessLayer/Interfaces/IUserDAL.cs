using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Shared.Responses;

namespace DataAccessLayer.Interfaces
{
    public interface IUserDAL
    {
        Task<Response> Insert(User user);
        Task<Response> Update(User user);
        Task<Response> Delete(User user);
        Task<DataResponse<User>> GetAll();
        Task<SingleResponse<User>> GetById(int id);
        Task<SingleResponse<User>> GetByUsername(string username);

    }
}
