using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]

namespace MyNamespace
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
        }
    }
}
