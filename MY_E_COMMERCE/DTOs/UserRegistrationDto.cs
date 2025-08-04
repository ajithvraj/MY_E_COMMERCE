using System.ComponentModel.DataAnnotations;

namespace MY_E_COMMERCE.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } 

        public string? Phone { get; set; }

        public string? Address { get; set; }





    }
}
