using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MY_E_COMMERCE.DTOs;
using MY_E_COMMERCE.Services;
using System.Threading.Tasks;
namespace MY_E_COMMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthenticationController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("Registration")]

        public async Task<IActionResult> Register([FromBody] UserRegistrationDto model)
        {
            try
            {
                int userId = await _authService.RegisterUserAsync(model);
                return Ok(new { userId, Message = "Registration Successful" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            //int userId = await _authService.RegisterUserAsync(model);
            //return Ok(new { userId, message = "Registration successfull " });


        }

        [HttpPost("User/login")]

        public async Task<IActionResult> login([FromBody]  UserLoginDto model)
        {

            //try
            //{
            //    var (user, token) = await _authService.LoginAsync(model);

            //    if (user == null || token == null)
            //    {
            //        return Unauthorized("Not a user,Please signup");

            //    }
            //    return Ok(new { Token = token, user = new { user.Email, user.Fullname, user.Role } });
            //}



            //catch (UnauthorizedAccessException ex)

            //{
            //    return StatusCode(403, ex.Message);
            //}



            var (user, token) = await _authService.LoginAsync(model);

            if (user == null || token == null)
            {
                throw new UnauthorizedAccessException("invalid credentials or user does not exist");



            }





            return Ok(new
            {
                Token = token,
                user = new { user.Email, user.Fullname, user.Role }
            });







        }

        [HttpPost("Admin/login")]
        public async Task<IActionResult> Adminlogin([FromBody] AdminLoginDto model)
        {

            try
            {
                var (user, token) = await _authService.AdminLoginAsync(model);


                if (user == null || token == null)
                {
                    return Unauthorized("only admin can login");
                }

                return Ok(new { Token = token, user = new { user.Fullname } });
            }

            catch (UnauthorizedAccessException ex)
            {

                return StatusCode(403, ex.Message);

            }
        }

        

        


            


    }
}

