using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
    public class CouponUsage:BaseEntity
    {
        [ForeignKey("Coupon")]
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;    
    }
}
