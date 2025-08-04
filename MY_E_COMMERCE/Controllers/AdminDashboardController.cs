using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MY_E_COMMERCE.DTOs.AdminDTOs;
using MY_E_COMMERCE.Services.Adminservices;

namespace MY_E_COMMERCE.Controllers

    
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]



    public class AdminDashboardController : ControllerBase
    {

        private readonly IAdminServices _adminServices;

        public AdminDashboardController(IAdminServices adminServices)
        {
            _adminServices = adminServices;

        }

        [HttpGet("GetAllUsers")]



        public async Task<IActionResult> GetAllUsers()
        {

            var users = await _adminServices.GetAllUsersAsync();

            return Ok(users);




        }

        [HttpPut("user-status")]

        public async Task<IActionResult> UserUpdate([FromBody] UserStatusDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminServices.UserStatusAsync(dto);

            if (result)
                return Ok(new { message = "user status updated successfully" });

            return BadRequest(result);



        }

        [HttpGet("find/{EmailOrUserId}")]

        public async Task<IActionResult> GetUsersByEmailOrUserId(string EmailOrUserId)
        {

            var user = await _adminServices.UserAccessByEmailOrUserId(EmailOrUserId);


            if (user == null)
            {
                return BadRequest("no user found");
            }

            return Ok(user);



        }

        [HttpDelete("{userId}")]

        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var result = await _adminServices.DeleteUserAsync(userId);

                if (!result)
                {
                    return NotFound(new { message = "User not found or could not be deleted " });
                }
                return Ok(new { message = "User deleted successfully" });

            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);

            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {

                return StatusCode(500, new { message = ex.Message });
            }












        }
    }


}
