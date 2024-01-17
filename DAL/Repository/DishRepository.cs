using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Enums;
using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.DAL.Repository
{
    public interface IDishRepository
    {
        Task<DishEntity> GetDish(string dishId);
        Task AddDish(DishEntity dishEntity);
        Task<List<DishEntity>> SortingDescByName(DishCategory category, Boolean vegetarian);
        Task<List<DishEntity>> SortingDescByPrice(DishCategory category, Boolean vegetarian);
        Task<List<DishEntity>> SortingDescByRating(DishCategory category, Boolean vegetarian);
        Task<List<DishEntity>> SortingAscByName(DishCategory category, Boolean vegetarian);
        Task<List<DishEntity>> SortingAscByPrice(DishCategory category, Boolean vegetarian);
        Task<List<DishEntity>> SortingAscByRating(DishCategory category, Boolean vegetarian);
    }
    public class DishRepository: IDishRepository
    {
        private readonly ApplicationDbContext _context;

        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DishEntity> GetDish(string dishId)
        {
            var dishEntity = await _context
                .Dish
                .Where(x => x.Id.ToString() == dishId)
                .FirstOrDefaultAsync();

            return dishEntity;
        }

        public async Task AddDish(DishEntity dishEntity)
        {
            await _context.Dish.AddAsync(dishEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DishEntity>> SortingDescByName(DishCategory category, Boolean vegetarian)
        {
            var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderByDescending(x => x.Name)
           .ToListAsync();

            return Alldish;
        }
        public async Task<List<DishEntity>> SortingDescByPrice(DishCategory category, Boolean vegetarian)
        {
            var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderByDescending(x => x.Price)
           .ToListAsync();

            return Alldish;
        }

        public async Task<List<DishEntity>> SortingDescByRating(DishCategory category, Boolean vegetarian)
        {
            var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderByDescending(x => x.Rating)
           .ToListAsync();

            return Alldish;
        }

        public async Task<List<DishEntity>> SortingAscByName(DishCategory category, Boolean vegetarian)
        {
            var Alldish = await _context
            .Dish
            .Where(x => x.Category == category && x.Vegetarian == vegetarian)
            .OrderBy(x => x.Name)
            .ToListAsync();

            return Alldish;
        }

        public async Task<List<DishEntity>> SortingAscByPrice(DishCategory category, Boolean vegetarian)
        {
            var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderBy(x => x.Price)
           .ToListAsync();

            return Alldish;
        }
        public async Task<List<DishEntity>> SortingAscByRating(DishCategory category, Boolean vegetarian)
        {
            var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderBy(x => x.Rating)
           .ToListAsync();

            return Alldish;
        }
    }
}
