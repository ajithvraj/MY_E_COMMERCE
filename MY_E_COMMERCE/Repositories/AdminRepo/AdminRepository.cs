using Dapper;
using System.Data;
using Microsoft.AspNetCore.Identity;
using MY_E_COMMERCE.DataAccess;
using MY_E_COMMERCE.DTOs.AdminDTOs;
using MY_E_COMMERCE.Model;

namespace MY_E_COMMERCE.Repositories.AdminRepo
{
    public class AdminRepository : IAdminRepository
    {

        private readonly ISqlConnectionFactory _connectionFactory; 


        public AdminRepository (ISqlConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            using var connection = _connectionFactory.CreateConnection();

            string sql = "select   USERID  , EMAIL ,FULLNAME , ROLE ,PHONE, ADDRESS , CREATEDAT,ISBlocked from Users";

            return await connection.QueryAsync<User>(sql);

        }

        public async Task<bool> UserStatusAsync(UserStatusDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = @" Update Users set IsBlocked = @IsBlocked where UserId = @UserId";


            var rowAffected = await connection.ExecuteAsync(query, new
            {

                dto.UserId,
                dto.IsBlocked

            });

            return rowAffected > 0;
        }

        public async Task<UserAccessDto> AccessUserByEmailAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();

           string query =  "select FullName,UserId,Email,Address,Phone,Role,IsBlocked from Users where email = @Email";

            return await connection.QueryFirstOrDefaultAsync<UserAccessDto>(query, new {email });

        }


        public async Task<UserAccessDto>AccessUserByIdAsync(int userId)
        {
            var connection = _connectionFactory.CreateConnection();

            string sql = "select FullName,UserId,Email,Address,Phone,Role,IsBlocked from Users where userId = @UserId";

            return await connection.QueryFirstOrDefaultAsync<UserAccessDto> (sql , new {userId});



        }

        public async Task<int> DeleteUserAsync(int userId)
        {

            using var connection = _connectionFactory.CreateConnection();
            string sql = "delete from Users where UserId = @UserId ";
            return await connection.ExecuteAsync(sql,new {UserId = userId});

        }


        public async Task<User> GetUserByIdAsync(int userId)
        {

            using var connection = _connectionFactory.CreateConnection();

            string sql = "select UserId , Role from Users where UserId = @UserId";

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new {UserId =  userId });
          

        }

       




    }
}
