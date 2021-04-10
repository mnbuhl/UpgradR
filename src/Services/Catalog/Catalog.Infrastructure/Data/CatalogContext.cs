using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IMongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);

            Products = database.GetCollection<Product>(config.CollectionName);

            CatalogContextSeed.SeedData(Products).Wait();
        }

        public IMongoCollection<Product> Products { get; }
    }
}