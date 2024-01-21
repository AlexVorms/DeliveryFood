using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebApplication2.Configurations;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;
using WebApplication2.DAL.Repository;

namespace WebApplication2.Services
{
    public interface IAuthService
    {
        Task<Boolean> RegisterUser(UserDto model);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       
        public async Task<Boolean> RegisterUser(UserDto model)
        {
            var userEntity = await _userRepository.GetUserByEmailAndPassword(model.Email, model.Password);

            if (userEntity != null)
            {
                return false;
            }
            else
            {
                var userModel = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    FullName = model.FullName,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    Password = model.Password,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    IsAdmin = false
                };
                await _userRepository.AddUser(userModel);
                return true;
            }
        }

    }
}