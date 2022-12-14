using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApplication2.DAL.Enums;
using WebApplication2.DAL.Models;
using WebApplication2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WebApplication2.Exceptions;

namespace WebApplication2.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishService _dishService;
        private IRatingService _ratingService;

        public DishController(IDishService dishService, IRatingService ratingService)
        {
            _dishService = dishService;
            _ratingService = ratingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDish(DishCategory dishCategory, Boolean vegetarian, int page, Sorting sorting)
        {
            try
            {
                if (page < 1 || page > 4)
                {
                    var response = new ResponseDto
                    {
                        Status = "Ошибка",
                        Message = "Недопустимое значение для страницы атрибута"
                    };
                    return BadRequest(response);
                }
                else
                {
                    var n = await _dishService.GetPage(page, dishCategory, vegetarian, sorting);
                    return Ok(n);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoDish(Guid id)
        {
            try
            {
                
                var result = await _dishService.GetInfoDish(id);
                if (result == null)
                {
                    var response = new ResponseDto
                    {
                        Status = "error",
                        Message = "Данного блюда не существует в меню"
                    };
                    return NotFound(response);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> AddDish(DishDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return StatusCode(401, "User model is incorrect");
        //    }
        //    try
        //    {
        //        await _dishService.AddDish(model);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Something went wrong during adding a User model");
        //    }
        //}
        [HttpGet]
       [Authorize]
        [Route("{id}/rating/check")]
        public async Task<IActionResult> ChecksRating(string id)
        {
            try
            {
                return Ok(await _ratingService.ChecksRating(User.Identity.Name, id));
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("{id}/rating")]
        public async Task<IActionResult> AddRating(Guid id, double rating)
        {
            try
            {
                var status = await _ratingService.AddRating(User.Identity.Name, id, rating);
                if (status== 1)
                {
                    return Ok();
                }
                else if (status == 0)
                {
                    var response = new ResponseDto
                    {
                        Status = "Forbidden",
                        Message = "Пользователь уже оставил отзыв на данное блюдо"
                    };
                    return StatusCode(403, response);
                }
                else
                {
                    var response = new ResponseDto
                    {
                        Status = "Forbidden",
                        Message = "Пользователь не заказывал данное блюдо"
                    };
                    return StatusCode(403, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

    }
}
