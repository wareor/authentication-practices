using AuthenticationAsFilter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationAsFilter.Services;

namespace AuthenticationAsFilter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UsersService _usersService;


        public UserController(UsersService usersService)
        {
            this._usersService = usersService;
        }

        [Authorize]
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> Get()
        {
            return this._usersService.GetUsers();
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            var token = this._usersService.Authenticate(user.Email, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }
            var userInfo = this._usersService.GetUserByLogin(user.Email, user.Password);
            return Ok(new { token, userInfo });

        }
    }
}