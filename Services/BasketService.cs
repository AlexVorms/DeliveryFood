using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Repository;

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
        private readonly IBasketRepository _basketRepository;
        public BasketService(ApplicationDbContext context, IBasketRepository basketRepository)
        {
            _context = context;
            _basketRepository = basketRepository;

        }
        public async Task<Boolean> AddDishToBasket(string id, Guid dishId)
        {
            var userEntity = await _context
            .User
            .Where(x => x.Id.ToString() == id)
            .FirstOrDefaultAsync();

            var basketEntity = await _basketRepository.GetBasketEntityByDishId(dishId.ToString());

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
                    var basket = new BasketEntity
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        UserId = userEntity.Id.ToString(),
                        DishId = dishEntity.Id.ToString()
                    };
                    await _basketRepository.SaveBasket(basket);
                }
                else
                {
                    basketEntity.Amount += 1;
                    await _basketRepository.UpdateBasket(basketEntity);
                }
                return true;
            }
        }

        public async Task<List<BasketDishDto>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetUserBasket(id);

            var listDtos = new List<BasketDishDto>();

            foreach (var i in basket)
            {
                var dishEntity = await _context
                .Dish
                .Where(x => x.Id.ToString() == i.DishId)
                .FirstOrDefaultAsync();

                var dish = new BasketDishDto
                {
                    Id = dishEntity.Id.ToString(),
                    Name = dishEntity.Name,
                    Amount = i.Amount,
                    Price = dishEntity.Price,
                    TotalPrice = dishEntity.Price * i.Amount,
                    Image = dishEntity.Image
                };

                listDtos.Add(dish);
            }

                return listDtos;
        }
        public DishEntity GetDishDtos(DishEntity dishEntity)
        { 
            var dish = new DishEntity
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
            return dish;
        }

        public async Task<Boolean> DeleteDishInBasket(string UserId, string dishId)
        {
            var basketEntity = await _basketRepository.GetDishInBasket(UserId, dishId);

            if(basketEntity == null)
            {
                return false;
            }
            else
            {
                await _basketRepository.DeleteDishInBasket(basketEntity);
                return true;
            }
        }
    }
}
