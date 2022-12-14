using Entities;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IWebScrapperBLL
    {
        public Task<DataResponse<Product>> VerifyProfile(string profile);

        public Task<DataResponse<TagWithCount>> Scrape(string profile);
    }
}
