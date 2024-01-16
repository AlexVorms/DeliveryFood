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
        [Authorize]
        public async Task<IActionResult> GetBasket()
        {
            try
            {
                return Ok( await _basketService.GetBasket(User.Identity.Name));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }


        [HttpPost]
        [Route("dish/{dishId}")]
       [Authorize]
        public async Task<IActionResult> AddDishToBasketRequest(Guid dishId)
        {
            try
            { 
                var result = await _basketService.AddDishToBasket(User.Identity.Name, dishId);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    var response = new ResponseDto
                    {
                        Status = "404",
                        Message = "Данного блюда нет в меню"
                    };
                    return NotFound(response);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpDelete]
        [Route("dish/{dishId}")]
        [Authorize]
        public async Task<IActionResult> Delete(string dishId)
        {
            try
            {
                var result = await _basketService.DeleteDishInBasket(User.Identity.Name, dishId);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    var response = new ResponseDto
                    {
                        Status = "404",
                        Message = "Данного блюда нет в корзине"
                    };
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }
    }

}
