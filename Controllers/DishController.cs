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
    [Route("api/[controller]")]
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
        public Task<IActionResult> GetDish(DishCategory dishCategory, Boolean vegetarian)
        {
            return null;
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
                return Ok(_dishService.GenerateDish());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }
        [HttpPost("{id}")]
        [Authorize]
        public async Task GetProfile(Guid id, double rating)
        {
            await _ratingService.AddRating(User.Identity.Name, id, rating);
        }
    }
}
