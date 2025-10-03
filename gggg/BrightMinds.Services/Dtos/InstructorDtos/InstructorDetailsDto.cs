using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.InstructorDtos
{
    public class InstructorDetailsDto
    {
     

        public string? Qualifications { get; set; }
       public string UserId { get; set; }
        public string? JobTitle { get; set; }
        public string DisplayName { get; set; }


        public string Mobile { get; set; }

        public string ImageCover { get; set; }
        public string Email { get; set; }



    }
}
