using System;
using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Enums;


namespace WebApplication2.DAL.Models
{
    public class DishBasketDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public string Image { get; set; }
    }
}
