using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Core.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}