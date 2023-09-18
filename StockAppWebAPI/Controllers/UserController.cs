using Microsoft.AspNetCore.Mvc;
using StockAppWebApi.Models;
using StockAppWebAPI.Services;

namespace StockAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)  
        {
            _userService=userService;
        }
        //https://localhost:port/api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var userId = await _userService.Register(user);
                return Ok(new { UserId = userId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
