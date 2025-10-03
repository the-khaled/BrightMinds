using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public decimal WaletBalance { get; set; } = 0;  
        public string? ImageCover { get; set; }
        public Instructor Instructor { get; set; }
    
    }
}
