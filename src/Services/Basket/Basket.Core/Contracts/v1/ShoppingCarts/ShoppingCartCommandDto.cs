using Basket.Core.Entities;
using System.Collections.Generic;

namespace Basket.Core.Contracts.v1.ShoppingCarts
{
    public class ShoppingCartCommandDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}