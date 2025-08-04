namespace MY_E_COMMERCE.DTOs.AdminDTOs
{
    public class AdminUserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IBlocked { get; set; }

        public string Role { get; set; }

    }
}
