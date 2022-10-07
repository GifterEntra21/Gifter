using DataAccessLayer.Interfaces;
using Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Impl
{
    public class UserDAL : IUserDAL
    {
        public async Task<SingleResponse<APIUser>> GetByUsername(APIUser model)
        {
            try
            {
                string query = $"SELECT * FROM c WHERE c.Username = '{model.Username}'";
                return await CosmosDb.GetSingleItem<APIUser>(query, "APIUsers");
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<APIUser>(ex);
            }

        }

        public async Task<SingleResponse<APIUser>> Login(APIUser model)
        {
            try
            {
                string query = $"SELECT * FROM c WHERE c.Username = '{model.Username}' AND c.Password = '{model.Password}'";
                return await CosmosDb.GetSingleItem<APIUser>(query, "APIUsers");
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
                //SingleResponse<APIUser> _user = await GetByUsername(user);

                return await CosmosDb.UpsertItem(user, "APIUsers");
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
