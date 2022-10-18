using Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IProductBLL
    {
        Task<Response> Insert(Product product);
        Task<Response> Upsert(Product updatedProduct);
        Task<Response> Delete(Product product);
        Task<DataResponse<Product>> GetAll();
        Task<DataResponse<Product>> GetByGenre(string genre);
        Task<DataResponse<Product>> GetByAssociatedPartner(string AssociatedPartner);
        Task<SingleResponse<Product>> GetById(string id);
        Task<Response> ClicksPlus(string productId);
    }
}
