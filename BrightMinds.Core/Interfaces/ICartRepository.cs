using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
                                     
    public interface ICartRepository: IGenericRepository<Cart>
    {
        public Task<Cart>GetCartByUserIdAsync(string userId);
    }
}
