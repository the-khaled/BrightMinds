
using BrightMinds.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(AppUser user,UserManager<AppUser>userManager);
    }
}
