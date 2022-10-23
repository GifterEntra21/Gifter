using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogicalLayer.Interfaces;
using Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;


namespace Cache_DatabaseTimeTrigger
{
    public class UpdateClicks
    {

        public readonly IDistributedCache _Cache;

        public UpdateClicks( IDistributedCache cache)
        {
            _Cache = cache;
        }

        [FunctionName("RedisDatabaseTrigger")]
        public async Task Run([TimerTrigger("* 30 * * * *")]TimerInfo myTimer, ILogger log)
        {

            string a = await _Cache.GetStringAsync("KeyPadrao");

            Dictionary<string,int> redisCache = JsonSerializer.Deserialize<Dictionary<string, int>>(a);

            foreach (var item in redisCache)
            {
                string id = item.Key;
                string query = $"SELECT * FROM c WHERE c.id = '{id}'";
                var c = await CosmosDB.GetSingleItem<Product>(query, "Products");
                if (c.HasSucces)
                {
                    c.Item.Clicks += item.Value;
                    var d = await CosmosDB.UpsertItem(c.Item, "Products");
                    if (d.HasSuccess)
                    {
                        redisCache[id] = 0;
                    }
                }
            }
            string e = JsonSerializer.Serialize(redisCache);
            await _Cache.SetStringAsync("KeyPadrao", e);
            await _Cache.SetStringAsync("Profiles", "       ");
        }
    }
}
