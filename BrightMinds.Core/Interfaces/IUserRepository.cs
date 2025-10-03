using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(string userId); 
        void Update(AppUser user);
    }
}
