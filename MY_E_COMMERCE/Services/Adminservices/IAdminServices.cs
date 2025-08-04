using MY_E_COMMERCE.DTOs;
using MY_E_COMMERCE.DTOs.AdminDTOs;
using MY_E_COMMERCE.Model;

namespace MY_E_COMMERCE.Services.Adminservices
{
    public interface IAdminServices
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<bool> UserStatusAsync(UserStatusDto dto);
        Task<UserAccessDto> UserAccessByEmailOrUserId(string input);

        Task<bool> DeleteUserAsync(int userId);
       
    }
}
