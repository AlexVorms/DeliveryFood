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
            return _basketService.GetBasket("13b5ffe6-ade5-4079-8d00-82bacab1da00");
        }


        [HttpPost]
        [Route("dish")]
       // [Authorize]
        public async Task Post(Guid dishId)
        {
            await _basketService.AddBasket("13b5ffe6-ade5-4079-8d00-82bacab1da00", dishId);
        }

        [HttpDelete]
        [Route("dish/{dishId}")]
        //[Authorize]
        public async Task Delete(string dishId)
        {
            await _basketService.DeleteDishInBasket("13b5ffe6-ade5-4079-8d00-82bacab1da00", dishId);
        }
    }

}
