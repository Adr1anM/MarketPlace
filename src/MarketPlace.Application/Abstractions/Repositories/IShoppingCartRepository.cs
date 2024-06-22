using MarketPlace.Domain.Models;


namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetCartByUserIdWithIncludeAsync(int userId);
        Task<ShoppingCart> AddToCartAsync(int userId, int productId, int quantity);
        Task<ShoppingCart> RemoveFromCartAsync(int userId, int productId);
        Task<ShoppingCart> UpdateCartItemQuantityAsync(int userId, int productId, int quantity);
        Task<ShoppingCartItem> GetShoppingCartItemAsync(ShoppingCart cart, int productId);
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(int userId);

    }
}
