using Microsoft.AspNetCore.Mvc;
using TopUpAPI.Dto;
using TopUpAPI.Services.UserService;
using TopUpAPI.Models;

namespace TopUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost()]
        public async Task<ActionResult<ResponseModel<GetUserDto>>> AddUser(
            [FromBody] AddUserDto user
        )
        {
            var response = await _userService.AddUserAsync(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}