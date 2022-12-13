using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class OrderDto
    {
        public DateTime DeliveryTime { get; set; }  
        public DateTime OrderTime { get; set; }
        public Status Status { get; set; }
        public double Price { get; set; }
        public Guid Id { get; set; }
    }
}
