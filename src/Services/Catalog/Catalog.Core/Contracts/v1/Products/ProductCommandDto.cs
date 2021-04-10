using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Contracts.v1.Products
{
    public class ProductCommandDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageFile { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}