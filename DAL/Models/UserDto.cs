using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class UserDto
    {
        [Required]
        [MinLength(1)]
        public string FullName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [MinLength(1)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
