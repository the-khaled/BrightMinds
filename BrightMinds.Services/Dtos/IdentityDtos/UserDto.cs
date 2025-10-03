using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.IdentityDtos
{
     public class UserDto
    {
        public string Id { get; set; } 
        public decimal WaletBalance { get; set; } = 0;
        public string DisplayName { get; set; }
     
     
        public string Mobile { get; set; }
     
        public string ImageCover { get; set; }
        public string Email { get; set; }
        //public string Token { get; set; }
    }
}
