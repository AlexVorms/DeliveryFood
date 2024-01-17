using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Entities;

namespace WebApplication2.DAL.Repository
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUser(string userId);
        Task<UserEntity> GetUserByEmailAndPassword(string email, string password);
        Task AddUser(UserEntity user);
        Task UpdateUser(UserEntity user);
    }
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserEntity> GetUser(string userId)
        {
            var userEntity = await _context
            .User
            .Where(x => x.Id.ToString() == userId)
            .FirstOrDefaultAsync();

            return userEntity;
        }
        public async Task<UserEntity> GetUserByEmailAndPassword(string email, string password)
        {
            var userEntity = await _context
          .User
          .Where(x => x.Email == email && x.Password == password)
          .FirstOrDefaultAsync();

            return userEntity;
        }
        public async Task AddUser(UserEntity user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserEntity user)
        {
            await _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
