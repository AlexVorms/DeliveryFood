using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class UserProfileDto
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
        public Gender Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
