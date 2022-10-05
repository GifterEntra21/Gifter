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
            try
            {
                ProductDAL productDAL = new();
                return await productDAL.GetAll();

            }   
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }
        }

        public async Task<DataResponse<Product>> GetByAssociatedPartner(string AssociatedPartner)
        {
            try
            {
                ProductDAL productDAL = new();
                return await productDAL.GetByAssociatedPartner(AssociatedPartner);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }
        }

        public async Task<DataResponse<Product>> GetByGenre(string genre)
        {
            try
            {
                ProductDAL productDAL = new();
                return await productDAL.GetByGenre(genre);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }
        }

        public async Task<SingleResponse<Product>> GetById(string id)
        {
            try
            {
                ProductDAL productDAL = new();
                return await productDAL.GetById(id);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Product>(ex);
            }
        }

        public async Task<Response> Insert(Product product)
        {
            try
            {
                

                ProductDAL productDAL = new();
                return await productDAL.Insert(product);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Upsert(Product updatedProduct)
        {
            try
            {
                ProductDAL productDAL = new();
                return await productDAL.Upsert(updatedProduct);

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
