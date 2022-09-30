using DataAccessLayer.Interfaces;
using Entities;

using Shared.Responses;

namespace DataAccessLayer
{
    public class ProductDAL : IProduct
    {
        public async Task<DataResponse<Product>> GetAll()
        {
            return await CosmosDb.GetItemList<Product>("SELECT * FROM c", "Products");
        }

        public async Task<DataResponse<Product>> GetByAssociatedPartner(string AssociatedPartner)
        {
            string query = $"SELECT * FROM c WHERE c.AssociatedPartner = '{AssociatedPartner}'";
            return await CosmosDb.GetItemList<Product>(query, "Products");
        }

        public async Task<DataResponse<Product>> GetByGenre(string genre)
        {
            string query = $"SELECT * FROM c WHERE c.Genre = '{genre}'";
            return await CosmosDb.GetItemList<Product>(query, "Products");
        }

        public async Task<SingleResponse<Product>> GetById(string id)
        {
            string query = $"SELECT * FROM c WHERE c.id = '{id}'";
            return await CosmosDb.GetSingleItem<Product>(query, "Products");
        }

        public async Task<Response> Insert(Product product)
        {
            try
            {
                return await CosmosDb.InsertItem<Product>(product, "Products");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }


        }

        public async Task<Response> Update(string oldProductId, Product updatedProduct)
        {
            throw new NotImplementedException();
        }
        public async Task<Response> Delete(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
