
using Shared.Responses;
using Microsoft.Azure;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using Cache_DatabaseTimeTrigger.Cosmos;

namespace Cache_DatabaseTimeTrigger
{
    public class CosmosAzf : ICosmosDbAzf
    {

        private readonly string _cosmosURI;
        private readonly string _CosmosPrimaryKey;


        private CosmosClient _client { get; set; }

        public CosmosAzf(string cosmosURI, string cosmosPrimaryKey)
        {
            _cosmosURI = cosmosURI;
            _CosmosPrimaryKey = cosmosPrimaryKey;
        }

        private async Task<Container> CosmosConnect(string containerName)
        {
            CosmosClient client = new(_cosmosURI, _CosmosPrimaryKey);

            _client = client;

            return client.GetContainer("GifterDb", containerName);
        }
        public async Task<Response> UpsertItem<T>(T updatedItem, string containerName)
        {
            try
            {
                Container _container = await CosmosConnect(containerName);
                await _container.UpsertItemAsync<T>(updatedItem);


                return ResponseFactory.CreateInstance().CreateSuccessResponse();

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
            finally
            {
                _client.Dispose();
            }
        }
        public async Task<SingleResponse<T>> GetSingleItem<T>(string query, string containerName)
        {
            try
            {
                Container _container = await CosmosConnect(containerName);

                FeedIterator<T> iterator = _container.GetItemQueryIterator<T>(query);

                FeedResponse<T> doc = await iterator.ReadNextAsync();

                T item = doc.FirstOrDefault();

                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<T>(item);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<T>(ex);
            }
            finally
            {
                _client.Dispose();
            }
        }
    }
}
