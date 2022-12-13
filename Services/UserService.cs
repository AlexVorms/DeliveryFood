using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetProfile(string id);
        Task EditUserProfile(string id, UserEditModel model);
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
                Address = userEntity.Address,
                PhoneNumber = userEntity.PhoneNumber
            };

            return userProfile;
        }
        public async Task EditUserProfile(string id, UserEditModel model)
        {
            var userEntity = await _context
                .User
                .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            if (userEntity == null)
            {

            }
            else
            {
                userEntity.FullName = model.FullName;
                userEntity.PhoneNumber = model.PhoneNumber;
                userEntity.BirthDate = model.BirthDate;
                userEntity.Gender = model.Gender;
                userEntity.Address = model.Address;
                await _context.SaveChangesAsync();
            }
        }
    }
}
