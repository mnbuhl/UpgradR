using Discount.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Discount.Api.Helpers
{
    public class NpgConnectionString : INpgConnectionString
    {
        public NpgConnectionString(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }

        public string ConnectionString { get; set; }
    }
}
