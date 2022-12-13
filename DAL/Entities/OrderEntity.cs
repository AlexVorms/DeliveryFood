using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Enums;
using WebApplication2.DAL.Models;

namespace WebApplication2.DAL.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string UserId { get; set; }
        public List<DishBasketDto> Basket { get; set; }
    }
}
