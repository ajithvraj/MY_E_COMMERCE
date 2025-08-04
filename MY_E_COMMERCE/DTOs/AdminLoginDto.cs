using System.ComponentModel.DataAnnotations;

namespace MY_E_COMMERCE.DTOs
{
    public class AdminLoginDto
    {

        [Required]

        public string Email { get; set; }

        [Required]
        public  string Password { get; set; }


    }
}
