namespace Catalog.Core.Interfaces
{
    public interface IMongoDbConfig
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}