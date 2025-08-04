using Azure.Messaging;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MY_E_COMMERCE.DTOs;
using MY_E_COMMERCE.Model;
using MY_E_COMMERCE.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MY_E_COMMERCE.Services
{
    public class AuthService : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly string[] _adminEmail = {"admin@stepupshoes.com"};

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            
        }
         



        public async Task<int> RegisterUserAsync(UserRegistrationDto model)

        {


            if(await _authRepository.UserExistingAsync(model.Email))
            {
                throw new Exception("There is an account associated with this email id");
            }

            var role = _adminEmail.Contains(model.Email.ToLower()) ? "Admin" : "User";

            //create user object with hashed password

            var user = new User
            {
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password), //hash password here
                Fullname = model.Fullname,
                Phone = model.Phone,
                Address = model.Address,
                Role = role


            };

            return await _authRepository.RegisterUserAsync(user); //save user to database





        }

        public async Task<(User? User, string? Token)> LoginAsync(UserLoginDto model)
        {






            ////getting user from database 

            var user = await _authRepository.GetUserByEmailAsync(model.Email);
            if (user == null)
                return (null,null );


            if(user.IsBlocked)
            {
                throw new UnauthorizedAccessException("User is blocked,Contact Admin");
            }






            //varify bcrytpted password




            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return (null, null);


            if  (user.Role != "User")
            {

                throw new UnauthorizedAccessException("Access strictly for Users");

            }
            
                
            

            var token = GenerateJwtToken(user);



            return (user, token);



           




        }


        public async Task<(User? User, string? Token)> AdminLoginAsync(AdminLoginDto model)
        {
            var user = await _authRepository.GetUserByEmailAsync(model.Email);
            if(user == null)
                return (null, null);
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return (null, null);

            if(user.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Not an admin");

                
            }
            var token = GenerateJwtToken(user);
            return (user, token);
        }
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role) // Add role claim
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }







    }
}
