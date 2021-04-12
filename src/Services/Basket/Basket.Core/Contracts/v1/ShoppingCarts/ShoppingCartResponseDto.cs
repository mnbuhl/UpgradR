using Basket.Core.Entities;
using System.Collections.Generic;

namespace Basket.Core.Contracts.v1.ShoppingCarts
{
    public class ShoppingCartResponseDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                Items.ForEach(item => total += item.Price * item.Quantity);
                return total;
            }
        }
    }
}
