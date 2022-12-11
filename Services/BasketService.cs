using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Services
{
    public interface IBasketService
    {
        Task AddBasket(string id, Guid dishId);
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
                .Where(x => x.Dish.Id == dishId)
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
                    User = userEntity,
                    Dish = dishEntity
                });
            }
            else
            {
                 basketEntity.Amount += 1;
            }
            await _context.SaveChangesAsync();
        }

    }
}
