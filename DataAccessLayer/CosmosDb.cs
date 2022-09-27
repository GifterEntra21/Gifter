using Entities;
using Microsoft.Azure.Cosmos;
using Shared.Responses;

namespace DataAccessLayer
{
    public class CosmosDb
    {
        private static string CosmosEndpoint = "https://gifter-cosmos.documents.azure.com:443/";
        private static string CosmosPrimaryKey = "z0NHDlMzcRt0UYPl8erRzTiGQdJL6jJfiPjN5LbG34csnVljrwBX4noolwNH68I3I6L1W8KjqFGOePVjzE0Y6g==";

        private static async Task<DataResponse<T>> DataConnectAndQuery<T>(string query, string containerName)
        {
            CosmosClient client = new(CosmosEndpoint, CosmosPrimaryKey);

            Container container = client.GetContainer("GifterDb", containerName);

            FeedIterator<T> iterator = container.GetItemQueryIterator<T>(query);
            FeedResponse<T> doc = await iterator.ReadNextAsync();

            List<T> items = new List<T>();
            foreach (T item in doc)
            {
                items.Add(item);
            }

            return new DataResponse<T>("Sucesso", true, items, new Exception());
        }

        private static async Task<SingleResponse<T>> SingleConnectAndQuery<T>(string query, string containerName)
        {
            CosmosClient client = new(CosmosEndpoint, CosmosPrimaryKey);

            Container container = client.GetContainer("GifterDb", containerName);

            FeedIterator<T> iterator = container.GetItemQueryIterator<T>(query);

            FeedResponse<T> doc = await iterator.ReadNextAsync();

            T item = doc.FirstOrDefault(); 
            
            return new SingleResponse<T>("Sucesso", true, item, new Exception());

        }
        public static async Task<List<Product>> GetProducts(string genre)
        {
            string query = $"SELECT * FROM c WHERE c.Genre = '{genre}'";
            DataResponse<Product> response = await DataConnectAndQuery<Product>(query, "Products");

            return response.Item;
        }

        public static async Task<SocialMediaAccount> GetInstagramAccount()
        {
            string query = "SELECT * FROM c WHERE c.site = 'instagram.com'";
            SingleResponse<SocialMediaAccount> response = await SingleConnectAndQuery<SocialMediaAccount>(query, "WebScrapeDefaultAccounts");
             return response.Item;
   
        }
    }
}
