using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Impl
{
    public class UserBLL : IUserBLL
    {
        public readonly IUserDAL _userService;

        public UserBLL(IUserDAL userService)
        {
            _userService = userService;
        }

        public async Task<SingleResponse<APIUser>> GetByUsername(APIUser model)
        {
            try
            {
               return await _userService.GetByUsername(model);
            }
            catch(Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<APIUser>(ex);
            }
        }

        public async Task<SingleResponse<APIUser>> Login(APIUser model)
        {
            try
            {
                return await _userService.Login(model);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<APIUser>(ex);
            }
        }

        public async Task<Response> Update(APIUser user)
        {
            try
            {
                return await _userService.Update(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
