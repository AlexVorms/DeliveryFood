using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Repository;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetProfile(string id);
        Task EditUserProfile(string id, UserEditModel model);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }

        public async Task<UserProfileDto> GetProfile(string id)
        {
            var userEntity = await _userRepository.GetUser(id);
            
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
            var userEntity = await _userRepository.GetUser(id);

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

               await _userRepository.UpdateUser(userEntity);
            }
        }
    }
}
