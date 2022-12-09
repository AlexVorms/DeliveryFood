using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetProfile(string id);
    }
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDto> GetProfile(string id)
        {
            var userEntity = await _context
                .User
                .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            /*
             * вероятность сюда попасть - почти нулевая, т.к. мы не нашли пользователя по его ID из валидного токена
             * считаю, что это ошибка 401
            */
            if (userEntity == null)
            {
               
            }

            var userProfile = new UserProfileDto
            {
                FullName = userEntity.FullName,
                Email = userEntity.Email,
                Id = userEntity.Id,
                BirthDate = userEntity.BirthDate,
                Gender = userEntity.Gender,
                Address= userEntity.Address,
                PhoneNumber= userEntity.PhoneNumber
            };

            return userProfile;
        }
    }
}
