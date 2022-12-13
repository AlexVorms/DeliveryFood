using WebApplication2.DAL.Enums;

namespace WebApplication2.DAL.Models
{
    public class UserEditModel
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
