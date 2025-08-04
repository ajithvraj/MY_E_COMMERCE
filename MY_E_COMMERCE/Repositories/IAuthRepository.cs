using System.Data;
using Microsoft.Data.SqlClient;
using MY_E_COMMERCE.Model;
using System.Threading.Tasks;
using MY_E_COMMERCE.DTOs;

namespace MY_E_COMMERCE.Repositories
{
    public interface IAuthRepository
    {

        Task<int> RegisterUserAsync(User user);
        Task<bool> UserExistingAsync(string email);
         Task<User?> GetUserByEmailAsync(string email);


    }
}
