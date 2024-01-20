using System.Threading.Tasks;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.DAL.Repository
{
    public interface IOrderRepository
    {
        Task AddOrder(OrderEntity order);
        Task<List<OrderEntity>> GetOrderList(string id);
        Task<OrderEntity> GetOrder(string OrderId);
        Task ChangeOrderStatus(string orderId);
        Task<List<OrderEntity>> GetOrderListWithBasket(string UserId);
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrder(OrderEntity order)
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderEntity>> GetOrderList(string id)
        {
            var orders = await _context
           .Order
           .Where(x => x.UserId == id)
           .ToListAsync();
            return orders;
        }
        public async Task<OrderEntity> GetOrder(string OrderId)
        {
            var order = await _context
             .Order
             .Include(x => x.Basket)
            .Where(x => x.Id.ToString() == OrderId)
             .FirstOrDefaultAsync();

            return order;
        }

        public async Task<List<OrderEntity>> GetOrderListWithBasket(string UserId)
        {
            var ListOrders = await _context
            .Order
            .Include(x => x.Basket)
            .Where(x => x.UserId == UserId)
            .ToListAsync();

            return ListOrders;
        }

        public async Task ChangeOrderStatus(string orderId)
        {
           var order = await GetOrder(orderId);
            order.Status = DAL.Enums.Status.Delivered;
            await _context.SaveChangesAsync();
        }
    }
}
