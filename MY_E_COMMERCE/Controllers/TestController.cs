using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MY_E_COMMERCE.Repositories;

namespace MY_E_COMMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestRepository _repo;

        public TestController (TestRepository repo)
        {
            _repo = repo;
        }

        [HttpGet ("test-db")]

        //public async Task<IActionResult> TestDb()
        //{
        //    var isconnected = await _repo.TestConnectionAsync();
        //    return isconnected ? Ok("db connected ") : StatusCode(500, "bd failed");

        //}

        public IActionResult TestDb()
        {
            try
            {
                var result = _repo.TestConnection();
                return Ok("db connected ");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"error : {ex.Message}");
            }
        }




    }
}
