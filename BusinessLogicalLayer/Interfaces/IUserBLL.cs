using Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IUserBLL
    {
        Task<Response> Update(APIUser user);
        Task<SingleResponse<APIUser>> Login(APIUser model);
        Task<SingleResponse<APIUser>> GetByUsername(APIUser model);

    }
}
