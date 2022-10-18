using DataAccessLayer.Interfaces;
using Entities;
using Entities.Interfaces;
using Shared.Responses;

namespace DataAccessLayer.Impl
{
    public class ProductDAL : IProductDAL
    {
        public readonly ICosmosDB _CosmosService;

        public ProductDAL(ICosmosDB cosmosService)
        {
            _CosmosService = cosmosService;
        }

        public async Task<DataResponse<Product>> GetAll()
        {
            try
            {
                return await _CosmosService.GetItemList<Product>("SELECT * FROM c", "Products");

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
                string query = $"SELECT * FROM c WHERE c.AssociatedPartner = '{AssociatedPartner}'";
                return await _CosmosService.GetItemList<Product>(query, "Products");
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
                string query = $"SELECT * FROM c WHERE c.Genre = '{genre}'";
                DataResponse<Product> productsData = await _CosmosService.GetItemList<Product>(query, "Products");
                List<Product> productsOrderedByClicks = productsData.ItemList.OrderBy(p => p.Clicks).ToList();
                
                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<Product>(productsOrderedByClicks);

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
                string query = $"SELECT * FROM c WHERE c.id = '{id}'";
                return await _CosmosService.GetSingleItem<Product>(query, "Products");
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
                return await _CosmosService.InsertItem<Product>(product, "Products");
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
                return await _CosmosService.UpsertItem<Product>(updatedProduct, "Products");
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
                return await _CosmosService.DeleteItem(product, "Products");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }


    }
}
