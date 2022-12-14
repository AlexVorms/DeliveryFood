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
            try
            {
                var result = _loginService.Token(model);
                if (result == null)
                {
                    var responce = new ResponseDto
                    {
                        Status = "Ошибка 400",
                        Message = "Неправильный email пользователя или пароль"
                    };
                    return BadRequest(responce);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(UserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Неккоректные данные");
                }
                else {
                    await _authService.RegisterUser(model);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpGet("profile")]
       [Authorize]//Данный Endpoint доступен только для авторизованных пользователей
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                return Ok(await _userService.GetProfile(User.Identity.Name));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }
        [HttpPut]
        [Authorize]
        [Route("profile")]
        public async Task<IActionResult> EditUserProfile(UserEditModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Неккоректные данные");
                }
                else
                {
                    await _userService.EditUserProfile(User.Identity.Name, user);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }
    }

}
