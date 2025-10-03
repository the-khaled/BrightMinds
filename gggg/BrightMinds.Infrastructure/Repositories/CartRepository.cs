
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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly BrightMindsContext _brightMindsContext;

        public CartRepository(BrightMindsContext brightMindsContext):base(brightMindsContext) 
        {
            _brightMindsContext = brightMindsContext;
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _brightMindsContext.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
