using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DAL.Entities
{
    public class RatingEntity
    {
        public Guid Id { get; set; }
        [Required]
        public virtual DishEntity Dish { get; set; }
        [Required]
        public virtual UserEntity User { get; set; }
        [Required]
        public double Rating { get; set; }
    }
}
