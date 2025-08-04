namespace MY_E_COMMERCE.DTOs.AdminDTOs
{
    public class UserAccessDto
    {

        public int UserId { get; set; }
        public string Email { get; set; }

        public string FullName {  get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }    

        public bool IsBlocked { get; set; }

      


    }
}
