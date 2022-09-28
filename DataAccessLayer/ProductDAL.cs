using DataAccessLayer.Interfaces;
using Entities;

using Shared.Responses;

namespace DataAccessLayer
{
    public class ProductDAL : IProductDAL
    {
        public async Task<Response> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<Product>> GetByAssociatedPartner(string AssociatedPartner)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<Product>> GetByGenre(string genre)
        {
            string query = $"SELECT * FROM c WHERE c.Genre = '{genre}'";
            return await CosmosDb.DataConnectAndQuery<Product>(query, "Products");
        }

        public async Task<SingleResponse<Product>> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Insert(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Update(string oldProductId, Product updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
