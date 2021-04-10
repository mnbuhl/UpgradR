using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedData(IMongoCollection<Product> products)
        {
            bool productsExist = await products.Find(p => true).AnyAsync();

            if (productsExist)
                return;

            await products.InsertManyAsync(GetPreconfiguredProducts());
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "AMD Ryzen 7 5800X",
                    Summary = "AMD's fastest 8 core processor for mainstream desktop, with 16 procesing threads",
                    Description = "8 cores optimized for high-FPS gaming rigs. Get the high-speed gaming performance of the world’s best desktop processor. AMD Ryzen Master Utility the Simple and Powerful Overclocking Utility for AMD Ryzen processors.",
                    ImageFile = "product-1.png",
                    Price = 449.00M,
                    Category = "CPU"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "AMD Ryzen 9 5900X",
                    Summary = "The world's best gaming desktop processor, with 12 cores and 24 processing threads",
                    Description = "12 cores optimized for high-FPS gaming rigs. Get the high-speed gaming performance of the world’s best desktop processor. AMD Ryzen Master Utility the Simple and Powerful Overclocking Utility for AMD Ryzen processors.",
                    ImageFile = "product-2.png",
                    Price = 749.00M,
                    Category = "CPU"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Intel Core i7-11700K",
                    Summary = "Rocket Lake 8-Core 3.6 GHz",
                    Description = "11th Gen Intel® Core™ i7-11700K unlocked desktop processor. Featuring Intel® Turbo Boost Max Technology 3.0 and PCIe Gen 4.0 support, unlocked 11th Gen Intel® Core™ desktop processors are optimized for enthusiast gamers and serious creators and help deliver high performance overclocking for an added boost.",
                    ImageFile = "product-3.png",
                    Price = 419.00M,
                    Category = "CPU"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "NVIDIA GeForce RTX 3080",
                    Summary = "10GB GDDR6X PCI Express 4.0 Graphics Card",
                    Description = "The GeForce RTX 3080 delivers the ultra performance that gamers crave, powered by Ampere—NVIDIA’s 2nd gen RTX architecture. It’s built with enhanced RT Cores and Tensor Cores, new streaming multiprocessors, and superfast G6X memory for an amazing gaming experience.",
                    ImageFile = "product-4.png",
                    Price = 699.99M,
                    Category = "GPU"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "MSI - AMD Radeon RX 6800 XT 16G",
                    Summary = "16GB GDDR6 - PCI Express 4.0 - Graphics Card",
                    Description = "The AMD Radeon™ RX 6800 XT graphics card, powered by AMD RDNA™ 2 architecture, featuring 72 powerful enhanced Compute Units, 128 MB of all new AMD Infinity Cache and 16GB of dedicated GDDR6 memory, is engineered to deliver ultra-high frame rates and serious 4K resolution gaming.",
                    ImageFile = "product-5.png",
                    Price = 649.00M,
                    Category = "GPU"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "ASUS - ROG STRIX B550-F GAMING",
                    Summary = "AM4 Socket USB 3.2 AMD Motherboard",
                    Description = "ROG Strix B550 Gaming series motherboards offer a feature-set usually found in the higher-end ROG Strix X570 Gaming series, including the latest PCIe® 4.0. With robust power delivery and effective cooling, ROG Strix B550 Gaming is well-equipped to handle 3rd Gen AMD Ryzen™ CPUs",
                    ImageFile = "product-6.png",
                    Price = 178.99M,
                    Category = "Motherboard"
                }
            };
        }
    }
}