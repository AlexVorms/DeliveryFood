using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication2.Configurations;
using WebApplication2.DAL.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ILoginService _loginService;
        private IAuthService _authService;
        private IUserService _userService;
        public UsersController(ILoginService loginService, IAuthService authService, IUserService userService)
        {
            _loginService = loginService;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Postlogin(LoginDto model)
        {
            return _loginService.Token(model);
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(UserDto model)
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

        [HttpGet("profile")]
        [Authorize]//Данный Endpoint доступен только для авторизованных пользователей
        public async Task<UserProfileDto> GetProfile()
        {
            return await _userService.GetProfile(User.Identity.Name);
        }
    }

}
