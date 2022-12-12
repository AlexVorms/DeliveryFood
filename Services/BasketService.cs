using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication2.Services
{
    public interface IBasketService
    {
        Task AddBasket(string id, Guid dishId);
        Task<List<DishBasketDto>> GetBasket(string id);
    }
    public class BasketService : IBasketService {
        private readonly ApplicationDbContext _context;
        public BasketService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddBasket(string id, Guid dishId)
        {
            var userEntity = await _context
            .User
            .Where(x => x.Id.ToString() == id)
            .FirstOrDefaultAsync();


            var basketEntity = await _context
                .Basket
                .Where(x => x.DishId == dishId.ToString())
                .FirstOrDefaultAsync();


            var dishEntity = await _context
                .Dish
                .Where(x => x.Id == dishId)
                .FirstOrDefaultAsync();

            if (basketEntity == null)
            {
                await _context.Basket.AddAsync(new BasketEntity
                {
                    Id = Guid.NewGuid(),
                    Amount = 1,
                    UserId = userEntity.Id.ToString(),
                    DishId = dishEntity.Id.ToString()
                });
                await _context.SaveChangesAsync();
            }
            else
            {
                 basketEntity.Amount += 1;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<DishBasketDto>> GetBasket(string id)
        {
           
            var userEntity = await _context
          .User
          .Where(x => x.Id.ToString() == id)
          .FirstOrDefaultAsync();

            var basket = await _context
           .Basket
           .Where(x => x.UserId == id)
           .ToListAsync();

            var listDtos = new List<DishBasketDto>();

            foreach (var i in basket)
            {
                var dishEntity = await _context
          .Dish
          .Where(x => x.Id.ToString() == i.DishId)
          .FirstOrDefaultAsync();

                var genreDto = new DishBasketDto
                {
                    Id = dishEntity.Id,
                    Name = dishEntity.Name,
                    Amount = i.Amount,
                    Price = dishEntity.Price,
                    TotalPrice = dishEntity.Price * i.Amount,
                    Image = dishEntity.Image
                };

                listDtos.Add(genreDto);
            }

                return listDtos;
        }
        public DishEntity GetDishDtos(DishEntity dishEntity)
        { 
            var genreDtos = new DishEntity
            {
                Id = dishEntity.Id,
                Name = dishEntity.Name,
                Description = dishEntity.Description,
                Price = dishEntity.Price,
                Image = dishEntity.Image,
                Vegetarian = dishEntity.Vegetarian,
                Rating = dishEntity.Rating,
                Category = dishEntity.Category
            };
            return genreDtos;
        }
    }
}
