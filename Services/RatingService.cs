using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;

namespace WebApplication2.Services
{
    public interface IRatingService
    {
        Task AddRating(string IdUser, Guid IdDish, double Rating);
        Task<Boolean> ChecksRating(string UserId, string DishId);
    }
    public class RatingServise: IRatingService
    {
        private readonly ApplicationDbContext _context;
        public RatingServise(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddRating(string IdUser, Guid IdDish, double Rating)
        {
            var userEntity = await _context
           .User
           .Where(x => x.Id.ToString() == IdUser)
           .FirstOrDefaultAsync();

            var dishEntity = await _context
           .Dish
           .Where(x => x.Id == IdDish)
           .FirstOrDefaultAsync();

           var model = new RatingEntity
            {
               Id = Guid.NewGuid(),
               Dish = dishEntity,
               User = userEntity,
               Rating = Rating
            };
            await _context.Rating.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> ChecksRating(string UserId,string DishId)
        {
            var rating = await _context
          .Rating
          .Where(x => x.User.Id.ToString() == UserId && x.Dish.Id.ToString() == DishId)
          .FirstOrDefaultAsync();
            if(rating == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
