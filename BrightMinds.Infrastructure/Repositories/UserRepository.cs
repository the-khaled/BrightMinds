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
    public class UserRepository: IUserRepository
    {
        private readonly BrightMindsContext _context;

        public UserRepository(BrightMindsContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByIdAsync(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public void Update(AppUser user)
        {
            _context.Users.Update(user);
        }
    }
}
