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
        public string DishId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
