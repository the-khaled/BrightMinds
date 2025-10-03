using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.CartCdos;
using BrightMinds.Services.ServiceResponse.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
    public interface ICartService
    {
        public Task<CartResponse> GetCartItemsAsync(string userId);
        public Task<CartResponse> AddToCartAsync(CartItemDto  cartItem,string userId);
        public Task<CartResponse>ClearCartAsync(int cartId);

        public Task<CartResponse> DeleteCartItemAsync(int cartItemId);
        public Task<CartResponse> UpdateCartItemQuantity(int id, int quantity);
    }
}
