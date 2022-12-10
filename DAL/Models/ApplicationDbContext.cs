using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;

namespace WebApplication2.DAL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<RatingEntity> Rating { get; set; }
        public DbSet<DishEntity> Dish { get; set; }
        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<DishBasketEntity> DishBasket { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishBasketEntity>().HasKey(u => new { u.DishId, u.UserId });
        }
    }
}
