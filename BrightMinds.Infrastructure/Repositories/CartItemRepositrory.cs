using BrightMinds.Infrastructure.DataAccess;
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Infrastructure.Repositories
{
    public  class CartItemRepositrory:GenericRepository<CartItem>,ICartItemRepository
    {
        private readonly BrightMindsContext _brightMindsContext;

        public CartItemRepositrory(BrightMindsContext brightMindsContext) : base(brightMindsContext)
        {
            _brightMindsContext = brightMindsContext;
        }

        public async Task ClearCartItems(int cartId)
        {
          await _brightMindsContext.CartItems.Where(c=>c.CartId==cartId)
                .ExecuteDeleteAsync();  
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartId(ISpecification<CartItem> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
    }
}
