using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        [HttpGet]
        public ActionResult <Basket> Get()
        {
            Basket user = new Basket();
            user.TotalPrice = 224;
            user.Id ="djddh123";
            user.Amount =3455;
            user.Image = "http//:Alex";
            user.Name = "Alex";
            user.Price = 1234;
            return user;
        }

        [HttpPost("{id}")]
        public string Get(string id)
        {
            return $"This is GET with id = {id}";
        }
        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            return $"This is DELETE = {id}";
        }
    }

}
