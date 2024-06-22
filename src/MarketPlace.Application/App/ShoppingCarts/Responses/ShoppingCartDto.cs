using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.ShoppingCarts.Responses
{
    public class ShoppingCartDto
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ShoppingCartItemDto> ShoppingCartItems { get; set; } = new List<ShoppingCartItemDto>();
    }
}
