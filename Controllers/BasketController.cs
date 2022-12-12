using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.Services;
using WebApplication2.DAL.Entities;

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
        //[Authorize]
        public Task<List<DishBasketDto>> GetBasket()
        {
            return _basketService.GetBasket("11eb4fe2-dce6-475f-8fab-e0199e72cc8e");
        }


        [HttpPost]
        [Route("dish")]
       // [Authorize]
        public async Task Post(Guid dishId)
        {
            await _basketService.AddBasket("11eb4fe2-dce6-475f-8fab-e0199e72cc8e", dishId);
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
