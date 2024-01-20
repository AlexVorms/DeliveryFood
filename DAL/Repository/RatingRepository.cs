using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;

namespace WebApplication2.DAL.Repository
{
    public interface IRatingRepository
    {
        Task<List<RatingEntity>> GetListUsersRatingsForDish(string dishId);
        Task UpdateDishRating(string dishId, double rating);
        Task AddUserRating(RatingEntity userRating);
        Task<RatingEntity> GetUserRatingForDish(string UserId, string DishId);
    }
    public class RatingRepository
    {
        private readonly ApplicationDbContext _context;
        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RatingEntity>> GetListUsersRatingsForDish(string dishId)
        {
            var ratingList = await _context
           .Rating
           .Where(x => x.Dish.Id.ToString() == dishId)
           .ToListAsync();

            return ratingList;
        }
        public async Task UpdateDishRating(string dishId, double rating)
        {
            var dish = await _context.Dish.Where(x => x.Id.ToString() == dishId).FirstOrDefaultAsync();
            dish.Rating = rating;
            await _context.SaveChangesAsync();
        }
        public async Task AddUserRating(RatingEntity userRating)
        {
            await _context.Rating.AddAsync(userRating);
            await _context.SaveChangesAsync();
        }
        public async Task<RatingEntity> GetUserRatingForDish(string UserId, string DishId)
        {
            var rating = await _context
          .Rating
          .Where(x => x.User.Id.ToString() == UserId && x.Dish.Id.ToString() == DishId)
          .FirstOrDefaultAsync();

           return rating;
        }
    }
}
