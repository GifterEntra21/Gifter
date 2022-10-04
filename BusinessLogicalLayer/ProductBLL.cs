using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.Interfaces;
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
            try
            {
                product.id = Guid.NewGuid().ToString();

                ProductDAL productDAL = new();
                return await productDAL.Insert(product);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Update(Product updatedProduct)
        {
            try
            {
                ProductDAL productDAL = new();
                return await productDAL.Update(updatedProduct);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<Response> Delete(Product product)
        {
            try
            {
                ProductDAL productDAL = new();
                return await productDAL.Delete(product);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
