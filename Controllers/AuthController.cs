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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Post(LoginDto model)
        {
            return (IActionResult)_loginService.Token(model);
        }

       

        [HttpGet]
        [Authorize]//Данный Endpoint доступен только для авторизованных пользователей
        [Route("test_login")]
        public IActionResult TestAuth()
        {
            return Ok($"Ваш логин:");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("test_admin")]
        public IActionResult TestAdmin()
        {
            return Ok($"Ваш логин:");
        }
    }

}
