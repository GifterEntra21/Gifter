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
        public readonly ICosmosDB _CosmosService;

        public UserDAL(ICosmosDB cosmosService)
        {
            _CosmosService = cosmosService;
        }

        public async Task<SingleResponse<APIUser>> GetByUsername(APIUser model)
        {
            try
            {
                string query = $"SELECT * FROM c WHERE c.Username = '{model.Username}'";
                return await _CosmosService.GetSingleItem<APIUser>(query, "APIUsers");
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
                SingleResponse<APIUser> _Login = await _CosmosService.GetSingleItem<APIUser>(query, "APIUsers");
                if (_Login.Item is null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<APIUser>(null, "Não foi possível fazer o Login");
                }
                return _Login;
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

                return await _CosmosService.UpsertItem(user, "APIUsers");
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
