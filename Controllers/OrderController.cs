using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("{id}")]
        // [Authorize]
        public async Task<OrderInfoDto> GetOrder(string id)
        {
            return await _orderService.GetOrder(id);
        }

        [HttpGet]
       // [Authorize]
        public async Task<List<OrderDto>> GetAllOrders()
        {
            return await _orderService.GetAllOrder("13b5ffe6-ade5-4079-8d00-82bacab1da00");
        }

        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> Post(OrderCreateDto order)
        {
            await _orderService.AddBasket("13b5ffe6-ade5-4079-8d00-82bacab1da00", order);
            return Ok("Заказ сформирован");
        }
        [HttpPost]
        //[Authorize]
        [Route("{id}/status")]
        public async Task PostStatus(string id)
        {
            await _orderService.ChangeOrderStatus(id);
        }
    }
}
