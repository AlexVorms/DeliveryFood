using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DAL.Entities
{
    public class BasketEntity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public virtual DishEntity Dish { get; set; }
        [Required]
        public virtual UserEntity User { get; set; }
    }
}
