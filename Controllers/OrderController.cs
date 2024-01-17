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
        [Authorize]
        public async Task<IActionResult> GetOrder(string id)
        {
            try
            {
                var result = await _orderService.GetOrder(id);
                if (result == null)
                {
                    var responce = new ResponseDto
                    {
                        Status = "Ошибка 404",
                        Message = "Данный заказ не существует"
                    };
                    return NotFound(responce);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpGet]
       [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                
                var result = await _orderService.GetAllOrder(User.Identity.Name);
                if (result == null)
                {
                    var responce = new ResponseDto
                    {
                        Status = "Ошибка 404",
                        Message = "Заказов не существует"
                    };
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch(Exception ex) 
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpPost]
      [Authorize]
        public async Task<IActionResult> Post(OrderCreateDto order)
        {
            try
            {
                
                var date = DateTime.Now;
                var time = (order.DeliveryTime.Year - date.Year)* 8760 + (order.DeliveryTime.Month - date.Month)*720 + (order.DeliveryTime.Day- date.Day)*24 + (order.DeliveryTime.Hour - date.Hour);
                if (time < 1)
                {
                    var responce = new ResponseDto
                    {
                        Status = "Ошибка 400",
                        Message = "Неверное время доставки. Время доставки должно быть больше текущей даты на 60 минут"
                    };
                    return BadRequest(responce);
                }
                else
                {
                    await _orderService.OrderFormation(User.Identity.Name, order);
                    return Ok("Заказ сформирован");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }
        [HttpPost]
        [Authorize]
        [Route("{id}/status")]
        public async Task<IActionResult> PostStatus(string id)
        {
            try
            {
                var result = await _orderService.ChangeOrderStatus(id);
                if (result == 0)
                {
                    var responce = new ResponseDto
                    {
                        Status = "Ошибка 404",
                        Message = "Такого заказа не существует"
                    };
                    return NotFound(responce);
                }
                else if (result == 1)
                {
                    var responce = new ResponseDto
                    {
                        Status = "Ошибка 400",
                        Message = "Заказ уже был доставлен"
                    };
                    return NotFound(responce);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }
    }
}
