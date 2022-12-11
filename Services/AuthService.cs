using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebApplication2.Configurations;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;

namespace WebApplication2.Services
{
    public interface IAuthService
    {
        Task RegisterUser(UserDto model);
        UserDto[] GenerateUsers();
        Task GenerateTMP();
    }
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task GenerateTMP()
        {
            var tmpModel = new UserDto
            {
                FullName = "alex",
                Email = "hella@mail.ru",
                BirthDate = DateTime.Now,
                Password = "12345",
                Gender = DAL.Enums.Gender.Male,
                PhoneNumber = "12345",
                Address = "random"
            };
            await RegisterUser(tmpModel);
            return;
        }
        public UserDto[] GenerateUsers()
        {
            return _context.User.Select(x=> new UserDto
            {   
                FullName = x.FullName,
                Email = x.Email,
                BirthDate = x.BirthDate,
                Password = x.Password,
                Gender = x.Gender,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address
            }).ToArray();
        }
        public async Task RegisterUser(UserDto model)
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
            await _context.User.AddAsync(userModel);
             await _context.SaveChangesAsync();

        }

    }
}