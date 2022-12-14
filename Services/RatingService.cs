using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;

namespace WebApplication2.Services
{
    public interface IRatingService
    {
        Task<Int32> AddRating(string IdUser, Guid IdDish, double Rating);
        Task<Boolean> ChecksRating(string UserId, string DishId);
    }
    public class RatingServise: IRatingService
    {
        private readonly ApplicationDbContext _context;
        public RatingServise(ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task Ratings(string id, DishEntity dish)
        {
            var ratingList = await _context
           .Rating
           .Where(x => x.Dish.Id.ToString() == id)
           .ToListAsync();

            double rating = 0;
            int number = 0;
            foreach (var i in ratingList)
            {
                rating += i.Rating;
                number += 1;
            }
            rating = rating / number;
            dish.Rating = rating;
            await _context.SaveChangesAsync();
        }
        public async Task<Int32> AddRating(string IdUser, Guid IdDish, double Rating)
        {
            var userEntity = await _context
           .User
           .Where(x => x.Id.ToString() == IdUser)
           .FirstOrDefaultAsync();

            var dishEntity = await _context
           .Dish
           .Where(x => x.Id == IdDish)
           .FirstOrDefaultAsync();

            var checks = await ChecksRating(IdUser, IdDish.ToString());
            if (checks)
            {
                return 0;
            }
            else
            {
                var checksOrder = await ChecksOrder(IdUser, IdDish.ToString());
                if (checksOrder)
                {
                    var model = new RatingEntity
                    {
                        Id = Guid.NewGuid(),
                        Dish = dishEntity,
                        User = userEntity,
                        Rating = Rating
                    };
                    await _context.Rating.AddAsync(model);
                    await _context.SaveChangesAsync();
                    await Ratings(IdDish.ToString(), dishEntity);
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
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
        public async Task<Boolean> ChecksOrder(string UserId, string DishId)
        {
            var flag = false;
            var ListOrders = await _context
        .Order
        .Include(x => x.Basket)
        .Where(x => x.UserId == UserId)
        .ToListAsync();
            foreach(var order in ListOrders)
            {
                var ListDish = order.Basket;
                foreach(var dish in ListDish)
                {
                    if(dish.DishId == DishId)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
    }
}
