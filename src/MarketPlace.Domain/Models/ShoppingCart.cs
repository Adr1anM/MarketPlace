using MarketPlace.Domain.Models.Auth;


namespace MarketPlace.Domain.Models
{
    public class ShoppingCart : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    }
}
