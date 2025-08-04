using System.Globalization;

namespace MY_E_COMMERCE.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public String PasswordHash { get; set; }
        public string Role { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }

        public Boolean IsBlocked { get; set; }

    }
}
