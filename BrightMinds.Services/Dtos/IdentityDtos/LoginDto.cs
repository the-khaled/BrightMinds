using System.ComponentModel.DataAnnotations;

namespace BrightMinds.Services.Dtos.IdentityDtos
{

    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }
}
