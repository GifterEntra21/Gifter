using DataAccessLayer.Interfaces;
using Entities;
using Entities.Interfaces;
using Shared.Responses;

namespace DataAccessLayer.Impl
{
    public class ProductDAL : IProductDAL
    {
        //public readonly IProductDAL _ProductService;

        //public ProductDAL(IProductDAL productService)
        //{
        //    _ProductService = productService;
        //}

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

        public async Task<Response> Upsert(Product updatedProduct)
        {
            try
            {
                return await CosmosDb.UpsertItem<Product>(updatedProduct, "Products");
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
                return await CosmosDb.DeleteItem(product, "Products");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }


    }
}
