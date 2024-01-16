using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication2.Services
{
    public interface IBasketService
    {
        Task<Boolean> AddDishToBasket(string id, Guid dishId);
        Task<List<BasketDishDto>> GetBasket(string id);
        Task<Boolean> DeleteDishInBasket(string UserId, string dishId);
    }
    public class BasketService : IBasketService {
        private readonly ApplicationDbContext _context;
        public BasketService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Boolean> AddDishToBasket(string id, Guid dishId)
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
            if (dishEntity == null)
            {
                return false;
            }
            else
            {
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
                return true;
            }
        }

        public async Task<List<BasketDishDto>> GetBasket(string id)
        {
            var basket = await _context
           .Basket
           .Where(x => x.UserId == id)
           .ToListAsync();

            var listDtos = new List<BasketDishDto>();

            foreach (var i in basket)
            {
                var dishEntity = await _context
          .Dish
          .Where(x => x.Id.ToString() == i.DishId)
          .FirstOrDefaultAsync();

                var genreDto = new BasketDishDto
                {
                    Id = dishEntity.Id.ToString(),
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

        public async Task<Boolean> DeleteDishInBasket(string UserId, string dishId)
        {
            var basketEntity = await _context
                           .Basket
                           .Where(x => x.DishId == dishId && x.UserId == UserId)
                           .FirstOrDefaultAsync();
            if(basketEntity == null)
            {
                return false;
            }
            else
            {
                _context.Basket.Remove(basketEntity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
