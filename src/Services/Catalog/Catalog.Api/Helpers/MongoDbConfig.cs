using Catalog.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Catalog.Api.Helpers
{
    public class MongoDbConfig : IMongoDbConfig
    {
        public MongoDbConfig(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            DatabaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            CollectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
        }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}