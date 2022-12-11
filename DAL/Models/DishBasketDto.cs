using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class DishBasketDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public int Amount { get; set; }
       // [Required]
        //public double TotalPrice = Price*Amount;
        [Required]
        public string Image { get; set; }
        
        
    }
}
