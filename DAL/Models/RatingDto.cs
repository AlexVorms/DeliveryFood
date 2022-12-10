using WebApplication2.DAL.Entities;

namespace WebApplication2.DAL.Models
{
    public class RatingDto
    {
        public double Rating { get; set; }
        public string Dish { get; set; }
        public string User { get; set; }
    }
}
