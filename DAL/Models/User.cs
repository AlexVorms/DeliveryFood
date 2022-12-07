namespace WebApplication2.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Age { get; set; }
        public string Login { get; set; }
        public string Email{ get; set; }
        public string Password { set; get; }
       
    }
}
