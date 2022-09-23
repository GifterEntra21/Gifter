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
        Task<Response> Insert(APIUser user);
        Task<Response> Update(APIUser user);
        Task<Response> Delete(APIUser user);
        Task<DataResponse<APIUser>> GetAll();
        Task<SingleResponse<APIUser>> GetById(int id);
        Task<SingleResponse<APIUser>> Login(APIUser model);
        Task<SingleResponse<APIUser>> GetByUsername(APIUser model);

    }
}
