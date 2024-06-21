using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Login>>> Get()
        {
            var logins = await _userService.GetLoginsAsync();
            return Ok(logins);
        }

        [HttpPost]
        public async Task<ActionResult<Login>> Post(Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.UserId) || string.IsNullOrEmpty(login.Provider))
            {
                return BadRequest("Invalid login data.");
            }

            await _userService.RecordLoginAsync(login);
            return CreatedAtAction(nameof(Get), new { id = login.Id }, login);
        }
    }
}