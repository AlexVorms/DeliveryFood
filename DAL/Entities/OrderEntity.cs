using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Enums;

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
        public int Price { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
