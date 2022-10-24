using Cache_DatabaseTimeTrigger;
using Cache_DatabaseTimeTrigger.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Config.Startup))]

namespace Config
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddDistributedRedisCache(opt =>
            {
                opt.Configuration = "RedisGifterDatabase2.redis.cache.windows.net:6380,password=HG9u9p9wcabUXm0nzGOWYOcbtFq7rnyAVAzCaBBxP50=,ssl=True,abortConnect=False";
            });
            builder.Services.AddTransient<ICosmosDbAzf>(opt =>
            {
                string URL = "https://gifter-cosmos.documents.azure.com:443/";
                string primaryKey = "z0NHDlMzcRt0UYPl8erRzTiGQdJL6jJfiPjN5LbG34csnVljrwBX4noolwNH68I3I6L1W8KjqFGOePVjzE0Y6g==";

                var cosmosClient = new CosmosAzf(URL, primaryKey);
                return cosmosClient;
            });


        }
    }
}
