using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.App.ShoppingCarts.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using MarketPlace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Infrastructure.Persistance.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart> , IShoppingCartRepository
    {
        public ShoppingCartRepository(ArtMarketPlaceDbContext context) : base(context)
        {
            
        }

        public async Task<ShoppingCart> AddToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await GetShoppingCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = await GetShoppingCartItemAsync(cart, productId);
            var product = await _context.Products.FindAsync(productId);

            if (cartItem == null)
            {
                
                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price * quantity
                };
                cart.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                cartItem.Price = product.Price * quantity;
            }

            cart.UpdatedDate = DateTime.UtcNow;

            return cart;
        }

        public async Task<ShoppingCart> GetCartByUserIdWithIncludeAsync(int userId)
        {
            return await _context.ShoppingCarts
                .Include(cart => cart.ShoppingCartItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task<ShoppingCart> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await GetShoppingCartByUserIdAsync(userId);

            if (cart == null) throw new Exception("Shopping cart not found");

            var cartItem = await GetShoppingCartItemAsync(cart, productId);

            if (cartItem != null)
            {
                _context.ShoppingCartItems.Remove(cartItem);
            }

            return cart;
        }

        public async Task<ShoppingCartItem> GetShoppingCartItemAsync(ShoppingCart cart,int productId)
        {
            return cart.ShoppingCartItems
              .FirstOrDefault(item => item.ProductId == productId);

        }

        public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(int userId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.ShoppingCartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<ShoppingCart> UpdateCartItemQuantityAsync(int userId, int productId, int quantity)
        {
            var cart = await GetShoppingCartByUserIdAsync(userId);

            if (cart == null) throw new Exception("Shopping cart not found");

            var cartItem = await GetShoppingCartItemAsync (cart, productId);    

            if (cartItem == null) throw new Exception("Product not found in cart");

            var product = await _context.Products.FindAsync(productId);

            cartItem.Quantity = quantity;
            cartItem.Price = product.Price * quantity;
            cart.UpdatedDate = DateTime.UtcNow;

            return cart;
        }

    }
}
