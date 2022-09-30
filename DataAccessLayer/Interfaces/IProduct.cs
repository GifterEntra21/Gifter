using Entities;
using Shared.Responses;

namespace DataAccessLayer.Interfaces
{
    public interface IProduct
    {
        Task<Response> Insert(Product product);
        Task<Response> Update(string oldProductId, Product updatedProduct);
        Task<Response> Delete(Product product);
        Task<DataResponse<Product>> GetAll();
        Task<DataResponse<Product>> GetByGenre(string genre);
        Task<DataResponse<Product>> GetByAssociatedPartner(string AssociatedPartner);
        Task<SingleResponse<Product>> GetById(string id);
    }
}
