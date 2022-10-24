using Entities;
using Entities.Interfaces;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICosmosDB
    {

        public Task<DataResponse<T>> GetItemList<T>(string query, string containerName);
        public Task<SingleResponse<T>> GetSingleItem<T>(string query, string containerName);

        public Task<Response> InsertItem<T>(T item, string containerName);

        public Task<Response> DeleteItem(ICosmosDbItem item, string containerName);

        public Task<Response> UpsertItem<T>(T updatedItem, string containerName);

        public Task<List<SocialMediaAccount>> GetDefaultInstagramAccount();

    }
}
