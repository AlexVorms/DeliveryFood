﻿using Microsoft.AspNetCore.Mvc;
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
    }
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
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