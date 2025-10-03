using BrightMinds.Core.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
    public class Coupon:BaseEntity
    {
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public decimal Value { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; } 
        public Course Course { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; } 
        public AppUser User { get; set; } 
    }
}
