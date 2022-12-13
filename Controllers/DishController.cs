using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApplication2.DAL.Enums;
using WebApplication2.DAL.Models;
using WebApplication2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
        public async Task<DishPagedListDto> GetDish(DishCategory dishCategory, Boolean vegetarian, int page, Sorting sorting)
        {
            return await _dishService.GetPage(page, dishCategory, vegetarian,sorting);
        }

        [HttpGet("{id}")]
        public async Task<DishDto> GetInfoDish(Guid id)
        {
            return await _dishService.GetInfoDish(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddDish(DishDto model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401, "User model is incorrect");
            }
            try
            {
                await _dishService.AddDish(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }
        [HttpGet]
       // [Authorize]
        [Route("{id}/rating/check")]
        public async Task<Boolean> ChecksRating(string id)
        {
            return await _ratingService.ChecksRating("13b5ffe6-ade5-4079-8d00-82bacab1da00", id);
        }

        [HttpPost]
        //[Authorize]
        [Route("{id}/rating")]
        public async Task GetProfile(Guid id, double rating)
        {
            await _ratingService.AddRating("13b5ffe6-ade5-4079-8d00-82bacab1da00", id, rating);
        }

    }
}
