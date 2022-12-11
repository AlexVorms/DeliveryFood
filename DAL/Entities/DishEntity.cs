using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Entities
{
    public class DishEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public bool Vegetarian { get; set; }
        [Required]
        public DishCategory Category { get; set; }
        [Required]
        public double Rating { get; set;}
    }
}
