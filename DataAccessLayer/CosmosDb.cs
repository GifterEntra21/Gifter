using Entities;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CosmosDb
    {
        private static string CosmosEndpoint = "https://gifter-cosmos.documents.azure.com:443/";
        private static string CosmosPrimaryKey = "z0NHDlMzcRt0UYPl8erRzTiGQdJL6jJfiPjN5LbG34csnVljrwBX4noolwNH68I3I6L1W8KjqFGOePVjzE0Y6g==";

        public static async Task<List<Product>> GetProducts(string genre)
        {
            List<Product> products = new List<Product>();

            using( CosmosClient client = new(CosmosEndpoint, CosmosPrimaryKey))
            {
                Container container = client.GetContainer("GifterDb", "Products");
                string query = "SELECT * FROM c WHERE c.Genre = '" + genre + "'";
                FeedIterator<Product> iterator = container.GetItemQueryIterator<Product>(query);
                var doc = await iterator.ReadNextAsync();

                foreach (Product p in doc)
                {
                    products.Add(p);
                }

            }


            return products;
        }

        public static async Task<SocialMediaAccount> GetInstagramAccount()
        {
            SocialMediaAccount sca = new();

             using( CosmosClient client = new(CosmosEndpoint, CosmosPrimaryKey))
            {
                Container container = client.GetContainer("GifterDb", "WebScrapeDefaultAccounts");
                string query = "SELECT * FROM c WHERE c.site = 'instagram.com'";
                FeedIterator<SocialMediaAccount> iterator = container.GetItemQueryIterator<SocialMediaAccount>(query);
                var doc = await iterator.ReadNextAsync();

                foreach (var item in doc)
                {
                    sca.Email = item.Email;
                    sca.Password = item.Password;
                }

            }
                return sca;


            
        }
    }
}
