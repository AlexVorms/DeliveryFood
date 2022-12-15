using System.ComponentModel.DataAnnotations;
using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class UserEditModel
    {
        [MinLength(4)]
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
