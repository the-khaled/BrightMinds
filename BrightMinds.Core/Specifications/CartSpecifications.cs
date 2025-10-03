
using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Specifications
{
    public class CartSpecifications:BaseSpecification<Cart>
    {
        public CartSpecifications()
        {
            Includes.Add(c => c.Items);
            //Includes.Add(c => c.Items.boo);
        }
    }
}
