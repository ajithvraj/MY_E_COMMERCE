using MY_E_COMMERCE.DataAccess;
using Dapper;
using System.Threading.Tasks;
using MY_E_COMMERCE.Model;
using Microsoft.Data.SqlClient;

namespace MY_E_COMMERCE.Repositories
{
    public class AuthRepository : IAuthRepository 
    {

        private readonly ISqlConnectionFactory _connectionFactory;

        public AuthRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> RegisterUserAsync(User user)
        {

            using var connection = _connectionFactory.CreateConnection();
            const string sql = "insert into Users (FullName,Email,PassWordHash,Phone,Role,Address) values (@FullName,@Email,@PassWordHash,@Phone,@Role,@Address); select cast (Scope_Identity() as int)";



            return await connection.ExecuteScalarAsync<int>(sql, user);

        }

        public async Task<bool> UserExistingAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "select count(1) from Users where email = @Email";
            return await connection.ExecuteScalarAsync<int>(sql, new { email = email }) > 0;
            
            
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = "select * from Users where Email = @email";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });

        }
        

















    }
}
