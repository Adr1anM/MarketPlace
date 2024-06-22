using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.ShoppingCarts.Responses
{
    public class ShoppingCartItemDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; } 
        public string ProductDescription { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
