using BrightMinds.Infrastructure.DataAccess;
using Library.Core.Interfaces;
using Library.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BrightMinds.Infrastructure.Repositories
{
    internal class CouponRepository: GenericRepository<Coupon>, ICouponRepository
    {
        private readonly BrightMindsContext _context;

        public CouponRepository(BrightMindsContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Coupon> GetByCodeAsync(string code) => await _context.Set<Coupon>().FirstOrDefaultAsync(c => c.Code == code);


        /*return await _context.Set<Coupon>().Include(c => c.Course).AsNoTracking().FirstOrDefaultAsync(c => c.Code == code);*/


        public async Task<bool> IsCouponUsedAsync(string code)
        {
            var coupon = await GetByCodeAsync(code);
            return coupon != null && coupon.IsUsed;
        }

        public async Task<IEnumerable<Coupon>> GenerateCouponsAsync( int count, decimal value)////
        {
            var coupons = new List<Coupon>();
            for (int i = 0; i < count; i++)
            {
                coupons.Add(new Coupon
                {
                    Code = Guid.NewGuid().ToString().Substring(0, 8),
                    IsUsed = false,
                    Value = value,
                    
                });
            }

            await _context.Set<Coupon>().AddRangeAsync(coupons);
            await _context.SaveChangesAsync();

            return coupons;
          }

      
    }
}