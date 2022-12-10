using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;

namespace WebApplication2.Services
{
    public interface IDishService
    { 
        Task AddDish(DishDto model);
        DishDto[] GenerateDish();
    }
    public class DishService : IDishService
    {
        private readonly ApplicationDbContext _context;
        public DishService(ApplicationDbContext context)
        {
            _context = context;
        }
        public DishDto[] GenerateDish()
        {
            return _context.Dish.Select(x => new DishDto
            {
                Id = x.Id,
                Name = x.Name,
                Description= x.Description,
                Price = x.Price,
                Image = x.Image,
                Vegetarian = x.Vegetarian,
                Rating = x.Rating,
                Category= x.Category
            }).ToArray();
        }

        public async Task AddDish(DishDto model)
        {
            await _context.Dish.AddAsync(new DishEntity
            {
                Id = Guid.NewGuid(),
               Name = model.Name,
               Description= model.Description,
               Price = model.Price,
               Image = model.Image,
               Vegetarian = model.Vegetarian,
               Rating = model.Rating,
               Category = model.Category
            });
            await _context.SaveChangesAsync();
        }
    }
}
