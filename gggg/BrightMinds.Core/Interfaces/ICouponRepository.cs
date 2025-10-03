using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
    public interface ICouponRepository: IGenericReopsitory<Coupon>
    { 
        Task<Coupon> GetByCodeAsync(string code);   
        Task<bool> IsCouponUsedAsync(string code);
        Task<IEnumerable<Coupon>> GenerateCouponsAsync(int courseId, int count, decimal value);
    }
}
