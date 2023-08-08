using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Async__Inn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser userService;
        public UsersController(IUser service)
        {
            userService = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto data)
        {
            var user = await userService.Register(data, this.ModelState);

            if (ModelState.IsValid)
            {
                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
            
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userService.Authenticate(loginDto.UserName, loginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            return user;
        }
    }
}
