namespace WebApplication2.DAL.Models
{
    public class Dish
    {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public string Image { get; set; }
            public bool Vegetarian { get; set; }
            public int Rating { get; set; }
            public string Category { get; set; }
      public class Pagination
        {
            public int Size { get; set; }
            public int Count { get; set; }
            public int Current { get; set; }
  
        }
    }
}
