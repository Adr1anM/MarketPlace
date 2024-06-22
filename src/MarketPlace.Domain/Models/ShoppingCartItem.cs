using System.ComponentModel.DataAnnotations.Schema;


namespace MarketPlace.Domain.Models
{
    public class ShoppingCartItem : Entity
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Price { get; set; }
    }
}
