namespace WebApplication2.DAL.Models
{
    public class DishPagedListDto
    {
        public List<DishDto> Dishes { get; set; }  
        public PaginationDto Pagination { get; set; }  
    }
}
