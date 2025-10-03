
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
    public class CartItem:BaseEntity
    {
        public int CourseId { get; set; }
        public decimal Price { get; set; }
     //   public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public Course Course { get; set; }
    }
}
