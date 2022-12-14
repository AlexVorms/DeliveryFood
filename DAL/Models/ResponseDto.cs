using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DAL.Models
{
    public class ResponseDto
    {
        [Required]
        public string Status { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
