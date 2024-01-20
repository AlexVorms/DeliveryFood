using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;
using WebApplication2.DAL.Repository;

namespace WebApplication2.Services
{
    public interface IOrderService
    {
        Task OrderFormation(string id, OrderCreateDto order);
        Task<List<OrderDto>> GetAllOrder(string id);
        Task<OrderInfoDto> GetOrder(string OrderId);
        Task<Int32> ChangeOrderStatus(string OrderId);
    }
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IDishRepository _dishRepository;
        public OrderService(ApplicationDbContext context,IOrderRepository orderRepository, IBasketRepository basketRepository, IDishRepository dishRepository)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _dishRepository = dishRepository;
        }
        public async Task OrderFormation(string id, OrderCreateDto order)
        {
           var basket = await GetBasket(id);
        
            if (basket == null)
            {
            }
            else
            {
                double price = 0;
                foreach (var i in basket)
                {
                    price += i.TotalPrice;
                }
                var orderEntity = new OrderEntity
                {
                    Id = Guid.NewGuid(),
                    DeliveryTime = order.DeliveryTime,
                    OrderTime = DateTime.Now,
                    Status = DAL.Enums.Status.InProcess,
                    Address = order.Address,
                    UserId = id,
                    Basket = basket,
                    Price = price
                };
                await _orderRepository.AddOrder(orderEntity);
            }
        }

        public async Task<List<DishBasketDto>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetUserBasket(id);

            var listDtos = new List<DishBasketDto>();

            foreach (var i in basket)
            {
                var dishEntity = await _dishRepository.GetDish(i.DishId);

                var basketDto = new DishBasketDto
                {
                    Id = Guid.NewGuid(),
                    DishId = dishEntity.Id.ToString(),
                    Name = dishEntity.Name,
                    Amount = i.Amount,
                    Price = dishEntity.Price,
                    TotalPrice = dishEntity.Price * i.Amount,
                    Image = dishEntity.Image
                };

                listDtos.Add(basketDto);

                await _basketRepository.DeleteDishInBasket(i);
            }
            return listDtos;
        }

        public async Task<List<OrderDto>> GetAllOrder(string id)
        {
            var orders = await _orderRepository.GetOrderList(id);

            if (orders == null)
            {
                return null;
            }
            else
            {
                var listDtos = new List<OrderDto>();

                foreach (var i in orders)
                {

                    var orderDto = new OrderDto
                    {
                        Id = i.Id,
                        DeliveryTime = i.DeliveryTime,
                        OrderTime = i.OrderTime,
                        Status = i.Status,
                        Price = i.Price
                    };

                    listDtos.Add(orderDto);
                }

                return listDtos;
            }
        }

        public async Task<OrderInfoDto> GetOrder(string OrderId)
        {
            var order = await _orderRepository.GetOrder(OrderId);

            if(order == null)
            {
                return null;
            }
            var orderEntity = new OrderInfoDto
            {
                Id = order.Id,
                Price = order.Price,
                DeliveryTime = order.DeliveryTime,
                OrderTime = order.OrderTime,
                Status = order.Status,
                Address = order.Address,
                DishBasket = order.Basket
            };
            return orderEntity;
        }

        public async Task<Int32> ChangeOrderStatus(string OrderId)
        {
            var order = await _orderRepository.GetOrder(OrderId);

            if (order == null)
            {
                return 0;
            }
            else
            {
                if (order.Status == DAL.Enums.Status.Delivered)
                {
                    return 1;
                }
                else
                {
                    await _orderRepository.ChangeOrderStatus(order.Id.ToString());
                    return 3;
                }
            }
        }
    }
}
