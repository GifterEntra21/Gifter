
using Shared.Responses;
using Microsoft.Azure;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;

namespace Cache_DatabaseTimeTrigger
{
    public class Cosmos
    {
        private static string CosmosEndpoint = "https://gifter-cosmos.documents.azure.com:443/";
        private static string CosmosPrimaryKey = "z0NHDlMzcRt0UYPl8erRzTiGQdJL6jJfiPjN5LbG34csnVljrwBX4noolwNH68I3I6L1W8KjqFGOePVjzE0Y6g==";

        private static async Task<Container> CosmosConnect(string containerName)
        {
            CosmosClient client = new(CosmosEndpoint, CosmosPrimaryKey);

            return client.GetContainer("GifterDb", containerName);
        }

        public static async Task<Response> UpsertItem<T>(T updatedItem, string containerName)
        {
            try
            {
                Container container = await CosmosConnect(containerName);
                await container.UpsertItemAsync<T>(updatedItem);


                return ResponseFactory.CreateInstance().CreateSuccessResponse();

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public static async Task<SingleResponse<T>> GetSingleItem<T>(string query, string containerName)
        {
            try
            {
                Container container = await CosmosConnect(containerName);

                FeedIterator<T> iterator = container.GetItemQueryIterator<T>(query);

                FeedResponse<T> doc = await iterator.ReadNextAsync();

                T item = doc.FirstOrDefault();

                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<T>(item);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<T>(ex);
            }

        }
    }
}
