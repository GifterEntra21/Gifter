using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogicalLayer.Interfaces;
using Cache_DatabaseTimeTrigger.Cosmos;
using Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shared.Responses;

namespace Cache_DatabaseTimeTrigger
{
    public class UpdateClicks
    {

        public readonly IDistributedCache _Cache;
        public readonly ICosmosDbAzf _cosmosService;

        public UpdateClicks(IDistributedCache cache, ICosmosDbAzf cosmosService)
        {
            _Cache = cache;
            _cosmosService = cosmosService;
        }

        [FunctionName("RedisDatabaseTrigger")]
        public async Task Run([TimerTrigger("* */60 * * * *")]TimerInfo myTimer, ILogger log)
        {

            string productCache = await _Cache.GetStringAsync("KeyPadrao");

            Dictionary<string,int> redisCache = JsonSerializer.Deserialize<Dictionary<string, int>>(productCache);

            foreach (var item in redisCache)
            {
                string id = item.Key;
                SingleResponse<Product> cosmosItem = await _cosmosService.GetSingleItem<Product>($"SELECT * FROM c WHERE c.id = '{id}'", "Products");
                if (cosmosItem.HasSucces)
                {
                    cosmosItem.Item.Clicks += item.Value;
                    var cosmosItemUpdated = await _cosmosService.UpsertItem(cosmosItem.Item, "Products");
                    if (cosmosItemUpdated.HasSuccess)
                    {
                        redisCache[id] = 0;
                    }
                }
            }
            string productCacheUpdated = JsonSerializer.Serialize(redisCache);
            await _Cache.SetStringAsync("KeyPadrao", productCache);
            await _Cache.SetStringAsync("Profiles", "       ");
        }
    }
}
