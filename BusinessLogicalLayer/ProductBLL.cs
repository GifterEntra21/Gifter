using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Entities;
using Shared.Responses;

namespace BusinessLogicalLayer
{
    public class ProductBLL : IProduct
    {
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
            throw new NotImplementedException();
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
        public async Task<Response> Delete(Product product)
        {
            try
            {
                return await CosmosDb.DeleteItem(product, "Products");

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
