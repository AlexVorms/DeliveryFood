using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private IAuthService _authService;
        public Users(IAuthService authService)
        {
            _authService= authService;
        }
        [HttpPost]
        [Route(template: "/account/register")]
        public async Task<IActionResult> Post(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401, "User model is incorrect");
            }
                try
                {
                    await _authService.RegisterUser(model);
                    return Ok(_authService.GenerateUsers());
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Something went wrong during adding a User model");
                }
        }

    }
}
