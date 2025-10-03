
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
    }
}
