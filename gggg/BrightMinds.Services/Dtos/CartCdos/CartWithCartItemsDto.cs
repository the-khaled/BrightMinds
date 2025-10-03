using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.CartCdos
{
     public class CartWithCartItemsDto
    {
        public int Id { get; set; }
        public IEnumerable<CartItemWithCourseDto> Items { get; set; }
    }
}
