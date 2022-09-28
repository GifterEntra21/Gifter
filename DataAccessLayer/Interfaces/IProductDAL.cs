using Entities;
using Shared.Responses;

namespace DataAccessLayer.Interfaces
{
    internal interface IProductDAL
    {
        Task<Response> Insert(Product product);
        Task<Response> Update(string oldProductId, Product updatedProduct);
        Task<Response> Delete(string id);
        Task<DataResponse<Product>> GetAll();
        Task<DataResponse<Product>> GetByGenre(string genre);
        Task<DataResponse<Product>> GetByAssociatedPartner(string AssociatedPartner);
        Task<SingleResponse<Product>> GetById(string id);
    }
}
