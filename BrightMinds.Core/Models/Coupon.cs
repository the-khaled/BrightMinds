using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class Coupon:BaseEntity
    {
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public decimal Value { get; set; }
        public ICollection<CouponUsage> CouponUsages { get; set; }
    }
}
