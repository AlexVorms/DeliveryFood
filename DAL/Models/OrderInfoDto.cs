using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class OrderInfoDto
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Address { get; set; }
        public List<DishBasketDto> DishBasket { get; set;}
    }
}
