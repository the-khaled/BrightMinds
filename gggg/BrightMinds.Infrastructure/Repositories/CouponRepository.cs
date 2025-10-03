using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightMinds.Infrastructure.Repositories;
using BrightMinds.Core.Interfaces;

namespace Library.Infrastructure.Repositories
{
    public  class CouponRepository: GenericRepository<Coupon>, ICouponRepository
    {
        private readonly BrightMindsContext _context;

        public CouponRepository(BrightMindsContext context):base(context){ 
            _context = context;
        }
       /* public async Task<IEnumerable<Coupon>> GetAllAsync()
        {
            return await _context.Set<Coupon>().Include(c => c.Course).ToListAsync();
        }*/

        public async Task<Coupon> GetByCodeAsync(string code)
        {
            return await _context.Set<Coupon>().Include(c => c.Course).AsTracking().FirstOrDefaultAsync(c => c.Code == code);
        }

   /*     public async Task AddAsync(Coupon coupon)
        {
            await _context.Set<Coupon>().AddAsync(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Coupon coupon)
        {
            _context.Set<Coupon>().Update(coupon);
            await _context.SaveChangesAsync();
        }*/

        public async Task<bool> IsCouponUsedAsync(string code) 
        {
            var coupon = await GetByCodeAsync(code);
            return coupon != null&& coupon.IsUsed;
        }

        public async Task<IEnumerable<Coupon>> GenerateCouponsAsync(int courseId, int count, decimal value)////
        {
            var coupons = new List<Coupon>();
            for (int i = 0; i < count; i++)
            {
                coupons.Add(new Coupon
                {
                    Code = Guid.NewGuid().ToString().Substring(0, 8), 
                    IsUsed = false,
                    Value = value,
                    CourseId = courseId
                });
            }

            await _context.Set<Coupon>().AddRangeAsync(coupons);
            await _context.SaveChangesAsync();

            return coupons;
        }
    }
}

    