
using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
    public interface ICartItemRepository:IGenericRepository<CartItem>
    {
        public Task<IEnumerable<CartItem>> GetCartItemsByCartId(ISpecification<CartItem> specification); 
        public Task ClearCartItems(int cartId);
    }
}
