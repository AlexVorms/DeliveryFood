using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class DishDto
    {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public string Image { get; set; }
            public bool Vegetarian { get; set; }
            public int Rating { get; set; }
            public DishCategory Category { get; set; }
      
    }
}
