using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.IServices
{
    public interface ICouponService
    {
        public  Task<IEnumerable<Coupon>> GenerateCoupons(int count, decimal value);
        public  Task<bool> ValidateCoupon(string code);
        public  Task UseCoupon(string code, string userId, int courseId);


    }
}
