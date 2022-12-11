using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        [Authorize]
        public string GetBasket()
        {
            return "GET";
        }


        [HttpPost]
        [Route("dish")]
        [Authorize]
        public async Task Post(Guid dishId)
        {
            await _basketService.AddBasket(User.Identity.Name, dishId);
        }

        [HttpDelete]
        [Route("dish/{dishId}")]
        [Authorize]
        public string Delete(string dishId)
        {
            return $"This is DELETE = {dishId}";
        }
    }

}
