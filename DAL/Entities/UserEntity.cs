using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        public string FullName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MinLength(6)]
        [RegularExpression(@"[a-zA-Z]+\w*@[a-zA-Z]+\.[a-zA-Z]+")]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string? Avatar { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
