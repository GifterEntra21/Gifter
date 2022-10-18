using DataAccessLayer.Interfaces;
using Entities;
using Entities.Interfaces;
using Microsoft.Azure.Cosmos;
using Shared.Responses;

namespace DataAccessLayer
{
    public class CosmosDb : ICosmosDB
    {
        string _cosmosURI;
        string _CosmosPrimaryKey;
        public CosmosDb()
        {
            DotNetEnv.Env.Load("../");

            _cosmosURI = Environment.GetEnvironmentVariable("COSMOS_URI");
            _CosmosPrimaryKey = Environment.GetEnvironmentVariable("COSMOS_PRIMARY_KEY");
        }

        private async Task<Container> CosmosConnect(string containerName)
        {
            CosmosClient client = new(_cosmosURI, _CosmosPrimaryKey);

            return client.GetContainer("GifterDb", containerName);
        }


        /// <summary>
        /// Make a simple query on the database and returns a DataResponse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<DataResponse<T>> GetItemList<T>(string query, string containerName)
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
        public async Task<SingleResponse<T>> GetSingleItem<T>(string query, string containerName)
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
        public async Task<Response> InsertItem<T>(T item, string containerName)
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

        public async Task<Response> DeleteItem(ICosmosDbItem item, string containerName)
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


        /// <summary>
        /// If the item already exists on the database, it is updated, if not, it is created 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="NewItem">The updated object to be sent, must have the ID unaltered</param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<Response> UpsertItem<T>(T updatedItem, string containerName)
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


        public async Task<SocialMediaAccount> GetDefaultInstagramAccount()
        {
            string query = "SELECT * FROM c WHERE c.site = 'instagram.com'";
            SingleResponse<SocialMediaAccount> response = await GetSingleItem<SocialMediaAccount>(query, "WebScrapeDefaultAccounts");
            return response.Item;

        }
    }
}
