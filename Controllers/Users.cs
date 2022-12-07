using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        [HttpPost]
        [Route(template: "/account/register")]
        public IActionResult Register([FromBody] User model)
        {
            return Ok(model);
        }

    }
}
