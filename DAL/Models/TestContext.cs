using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;

namespace WebApplication2.DAL.Models
{
    public class TestContext : DbContext
    {
        public DbSet<DishEntity> Dish { get; set; }
        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public TestContext(DbContextOptions<TestContext> options): base(options){
            Database.EnsureCreated();
        }
    }
}
