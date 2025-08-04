using MY_E_COMMERCE.DTOs.AdminDTOs;
using MY_E_COMMERCE.Model;

namespace MY_E_COMMERCE.Repositories.AdminRepo
{
    public interface IAdminRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<bool> UserStatusAsync(UserStatusDto dto );

        Task<UserAccessDto>AccessUserByEmailAsync( string email );
        Task<UserAccessDto>AccessUserByIdAsync(int  userId );

        Task<int>DeleteUserAsync(int userId);
        Task<User>GetUserByIdAsync(int userId);
      



    }
}
