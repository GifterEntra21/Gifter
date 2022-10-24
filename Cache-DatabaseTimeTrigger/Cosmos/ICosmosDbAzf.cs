using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Responses;
namespace Cache_DatabaseTimeTrigger.Cosmos
{
    public interface ICosmosDbAzf
    {
        public Task<Response> UpsertItem<T>(T updatedItem, string containerName);
        public Task<SingleResponse<T>> GetSingleItem<T>(string query, string containerName);

    }
}
