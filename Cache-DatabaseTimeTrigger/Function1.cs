using System;
using System.Collections.Generic;
using System.Text.Json;
using BusinessLogicalLayer.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;


namespace Cache_DatabaseTimeTrigger
{
    public class Function1
    {
        public readonly IProductBLL _productService;
        public readonly IDistributedCache _Cache;

        public Function1(IProductBLL productService, IDistributedCache cache)
        {
            _productService = productService;
            _Cache = cache;
        }

        [FunctionName("RedisDatabaseTrigger")]
        public void Run([TimerTrigger("1 * * * * *")]TimerInfo myTimer, ILogger log)
        {

            string a = _Cache.GetString("KeyPadrao");

            Dictionary<string,int> redisCache = JsonSerializer.Deserialize<Dictionary<string, int>>(a);


            log.LogInformation($"C# Timer trigger function executed at: {redisCache}");
        }
    }
}
