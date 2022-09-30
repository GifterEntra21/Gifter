using Entities;
using Entities.Interfaces;
using Microsoft.Azure.Cosmos;
using Shared.Responses;

namespace DataAccessLayer
{
    public class CosmosDb
    {
        private static string CosmosEndpoint = "https://gifter-cosmos.documents.azure.com:443/";
        private static string CosmosPrimaryKey = "z0NHDlMzcRt0UYPl8erRzTiGQdJL6jJfiPjN5LbG34csnVljrwBX4noolwNH68I3I6L1W8KjqFGOePVjzE0Y6g==";

        private static async Task<Container> CosmosConnect(string containerName)
        {
            CosmosClient client = new(CosmosEndpoint, CosmosPrimaryKey);

            return client.GetContainer("GifterDb", containerName);
        }


        /// <summary>
        /// Make a simple query on the database and returns a DataResponse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static async Task<DataResponse<T>> GetItemList<T>(string query, string containerName)
        {
            try
            {
                Container container = await CosmosConnect(containerName);

                FeedIterator<T> iterator = container.GetItemQueryIterator<T>(query);
                FeedResponse<T> doc = await iterator.ReadNextAsync();
                List<T> items = new List<T>();

                foreach (T item in doc)
                {
                    items.Add(item);
                }

                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<T>(items);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<T>(ex);
            }

        }

        /// <summary>
        /// Make a simple query on the database and returns a singleResponse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="containerName">CosmosDb container you want to connect</param>
        /// <returns></returns>
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

        /// <summary>
        /// Insert an item in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static async Task<Response> InsertItem<T>(T item, string containerName)
        {
            try
            {
                Container container = await CosmosConnect(containerName);
                await container.CreateItemAsync<T>(item);

                return ResponseFactory.CreateInstance().CreateSuccessResponse();

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }

        }

        public static async Task<Response> DeleteItem(ICosmosDbItem item, string containerName)
        {
            try
            {
                Container container = await CosmosConnect(containerName);
                await container.DeleteItemAsync<ICosmosDbItem>(item.id, new PartitionKey(item.PartitionKey));

                return ResponseFactory.CreateInstance().CreateSuccessResponse();

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public static async Task<SocialMediaAccount> GetInstagramAccount()
        {
            string query = "SELECT * FROM c WHERE c.site = 'instagram.com'";
            SingleResponse<SocialMediaAccount> response = await GetSingleItem<SocialMediaAccount>(query, "WebScrapeDefaultAccounts");
            return response.Item;

        }
    }
}
