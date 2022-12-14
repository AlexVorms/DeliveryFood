namespace WebApplication2.DAL.Models
{
    public class BasketDishDto
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public string Image { get; set; }
    }
}
