using MY_E_COMMERCE.DTOs;
using MY_E_COMMERCE.Model;
using System.Data;
using System.Threading.Tasks;

namespace MY_E_COMMERCE.Services
{
    public interface IAuthServices
    {

        Task<int> RegisterUserAsync(UserRegistrationDto model);
        Task<(User? User , string? Token)> LoginAsync(UserLoginDto model);

       Task<(User? User, string? Token)> AdminLoginAsync(AdminLoginDto model);





    }
}
