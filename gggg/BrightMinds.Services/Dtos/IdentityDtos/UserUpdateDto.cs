using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.IdentityDtos
{
    public class UserUpdateDto
    {
        public string Id { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
         
     
        [Required]
        [Phone]
        public string? Mobile { get; set; }
        [Required]
     
        public IFormFile Image { get; set; }
    


    }
}
