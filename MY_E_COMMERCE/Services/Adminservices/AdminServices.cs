using Microsoft.AspNetCore.Http.HttpResults;
using MY_E_COMMERCE.DataAccess;
using MY_E_COMMERCE.DTOs.AdminDTOs;
using MY_E_COMMERCE.Model;
using MY_E_COMMERCE.Repositories.AdminRepo;
using System.Reflection.Metadata.Ecma335;

namespace MY_E_COMMERCE.Services.Adminservices
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;


        public AdminServices (IAdminRepository adminRepository , IConfiguration configuration)
        {

            _adminRepository = adminRepository;
            _configuration = configuration;

        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {

            //var user = new User
            //{
            //    UserId = model.UserId,
            //    Fullname = model.FullName,
            //    Email = model.Email,
            //    IsBlocked = model.IBlocked,
            //    Role = model.Role



            //};

            return _adminRepository.GetAllUsersAsync();

        }



        public  Task<bool> UserStatusAsync (UserStatusDto dto)
        {

            var user = new User
            {

                UserId = dto.UserId,
                IsBlocked = dto.IsBlocked,
               // Fullname = dto.UserName

            };


            return _adminRepository.UserStatusAsync(dto);




           


        }

        public  Task<UserAccessDto> UserAccessByEmailOrUserId(string input)
        {

            if(int.TryParse(input , out int userId ))
            {
               return  _adminRepository.AccessUserByIdAsync(userId);
            }

            else
            {
               return  _adminRepository.AccessUserByEmailAsync(input);
            }





           



        }

        //public Task<int> DeleteUser(int userId)
        //{

        //   var user = _adminRepository.GetUserByIdAsync(userId);

          



        //}

        public async Task<bool> DeleteUserAsync(int userId)
        {

            var user = await _adminRepository.GetUserByIdAsync(userId);


            if(user == null)
            {
                throw new KeyNotFoundException("User Not Found");


            }

           if(user.Role == "Admin")
            {
                throw new InvalidOperationException("Admin users cannot be deleted");
            }

            var rowAffected = await _adminRepository.DeleteUserAsync(userId);

            return rowAffected > 0;


            






        }










    }
}
