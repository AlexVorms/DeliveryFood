using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DAL.Entities
{
    public class DishBasketEntity
    {
        [Required]
        public int Quantity { get; set; }
        public string DishId { get; set; }
        public string UserId { get; set; }
        public DishEntity Dish { get; set; }
        public UserEntity User { get; set; }

    }
}
