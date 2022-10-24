using DataAccessLayer.Interfaces;
using Entities;
using Entities.Interfaces;
using Microsoft.Azure.Cosmos;
using Shared.Responses;

namespace DataAccessLayer
{
    public class CosmosDb : ICosmosDB
    {


        private CosmosClient _client { get; set; }
        public CosmosDb(string cosmosURI,string CosmosPrimaryKey)
        {
            _client = new(cosmosURI, CosmosPrimaryKey);

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
                Container _container = _client.GetContainer("GifterDb", containerName);

                FeedIterator<T> iterator = _container.GetItemQueryIterator<T>(query);
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
            finally
            {
                _client.Dispose();
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
                Container _container = _client.GetContainer("GifterDb", containerName);

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
                Container _container = _client.GetContainer("GifterDb", containerName);
                await _container.CreateItemAsync<T>(item);

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

        public async Task<Response> DeleteItem(ICosmosDbItem item, string containerName)
        {
            try
            {
                Container _container = _client.GetContainer("GifterDb", containerName);
                await _container.DeleteItemAsync<ICosmosDbItem>(item.id, new PartitionKey(item.PartitionKey));

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
                Container _container = _client.GetContainer("GifterDb", containerName);
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


        public async Task<List<SocialMediaAccount>> GetDefaultInstagramAccount()
        {
            string query = "SELECT * FROM c WHERE c.site = 'instagram.com'";
            DataResponse<SocialMediaAccount> response = await GetItemList<SocialMediaAccount>(query, "WebScrapeDefaultAccounts");
            return response.ItemList;

        }
    }
}
