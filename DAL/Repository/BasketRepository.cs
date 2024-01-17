using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;

namespace WebApplication2.DAL.Repository
{
    public interface IBasketRepository
    {
        Task<BasketEntity> GetBasketEntityByDishId(string dishId);
        Task<List<BasketEntity>> GetUserBasket(string userId);
        Task<BasketEntity> GetDishInBasket(string UserId, string dishId);
        Task SaveBasket(BasketEntity basket);
        Task UpdateBasket(BasketEntity basket);
        Task DeleteDishInBasket(BasketEntity basketEntity);
    }
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _context;
        public BasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BasketEntity> GetBasketEntityByDishId(string dishId)
        {

            var basketEntity = await _context
                .Basket
                .Where(x => x.DishId == dishId)
                .FirstOrDefaultAsync();

            return basketEntity;
        }
        public async Task<List<BasketEntity>> GetUserBasket(string userId)
        {
            var basketList = await _context
            .Basket
            .Where(x => x.UserId == userId)
            .ToListAsync();

            return basketList;
        }

        public async Task<BasketEntity> GetDishInBasket(string UserId, string dishId)
        {
            var basketEntity = await _context
                         .Basket
                         .Where(x => x.DishId == dishId && x.UserId == UserId)
                         .FirstOrDefaultAsync();

            return basketEntity;
        }
        public async Task SaveBasket(BasketEntity basket)
        {
            await _context.Basket.AddAsync(basket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBasket(BasketEntity basket) 
        {
            _context.Basket.Update(basket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDishInBasket(BasketEntity basketEntity)
        {
            _context.Basket.Remove(basketEntity);
            await _context.SaveChangesAsync();
        }
    }
}
