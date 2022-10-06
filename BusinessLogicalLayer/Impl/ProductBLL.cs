using BusinessLogicalLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Impl;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.Interfaces;
using Shared.Responses;

namespace BusinessLogicalLayer.Impl
{
    public class ProductBLL : IProductBLL
    {

        public readonly IProductDAL _ProductService;

        public ProductBLL(IProductDAL productService)
        {
            _ProductService = productService;
        }

        public async Task<DataResponse<Product>> GetAll()
        {
            try
            {
                return await _ProductService.GetAll();

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

                return await _ProductService.GetByAssociatedPartner(AssociatedPartner);

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
                return await _ProductService.GetByGenre(genre);

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
                return await _ProductService.GetById(id);

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
                

                return await _ProductService.Insert(product);

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
                return await _ProductService.Upsert(updatedProduct);

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
                return await _ProductService.Delete(product);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}
