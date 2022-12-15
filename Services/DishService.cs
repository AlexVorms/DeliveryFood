using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication2.DAL.Enums;
using static Azure.Core.HttpHeader;
using Azure;


namespace WebApplication2.Services
{
    public interface IDishService
    {
        Task AddDish(DishDto model);
        Task<DishDto> GetInfoDish(Guid id);
        Task<DishPagedListDto> GetPage(int page, DishCategory category, Boolean vegetarian, Sorting sorting);
    }
    public class DishService : IDishService
    {
        private readonly ApplicationDbContext _context;
        public DishService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public async Task AddDish(DishDto model)
        {
            await _context.Dish.AddAsync(new DishEntity
            {
                Id = Guid.NewGuid(),
               Name = model.Name,
               Description= model.Description,
               Price = model.Price,
               Image = model.Image,
               Vegetarian = model.Vegetarian,
               Rating = 0,
               Category = model.Category
            });
            await _context.SaveChangesAsync();
        }
        public async Task<DishDto> GetInfoDish(Guid id)
        {
            var dishEntity = await _context
           .Dish
           .Where(x => x.Id == id)
           .FirstOrDefaultAsync();

            if (dishEntity == null)
            {
                return null;
            }
            else
            {
                var dish = new DishDto
                {
                    Id = dishEntity.Id,
                    Image = dishEntity.Image,
                    Description = dishEntity.Description,
                    Price = dishEntity.Price,
                    Name = dishEntity.Name,
                    Vegetarian = dishEntity.Vegetarian,
                    Category = dishEntity.Category,
                    Rating = dishEntity.Rating
                };
                return dish;
            }
        }
        public async Task<DishPagedListDto> GetPage(int page, DishCategory category, Boolean vegetarian, Sorting sorting)
        {
            List<DishEntity> dishList;
           if((int)sorting <= 2)
            {
                dishList = await SortingAsc(category, vegetarian, sorting);
            }
            else
            {
                dishList = await SortingDesc( category, vegetarian, sorting);
            }

            var dishCountInDb = dishList.Count();
            var size = 3;
            var count = (int)(dishCountInDb / size)+1;

            var skipCount =(int)((page - 1) * size);
            var takeCount = (skipCount + size);


            var dishEntities = dishList.Skip(skipCount)
                  .Take(takeCount);

            var pageInfo = new PaginationDto
            {
                Current = page,
                Count = count,
                Size = size
            };

            var dishElementDtos = new List<DishDto>();

            foreach (var i in dishEntities)
            {
                dishElementDtos.Add(await GetDishElementDto(i));
            }

            var result = new DishPagedListDto
            {
                Dishes = dishElementDtos,
                 Pagination = pageInfo
            };

            return result;
        }
        public async Task<DishDto> GetDishElementDto(DishEntity model)
        {
            var dishEntity = new DishDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Image = model.Image,
                Vegetarian = model.Vegetarian,
                Rating = model.Rating,
                Category = model.Category
            };
            return dishEntity;
        }
        public async Task<List<DishEntity>> SortingDesc(DishCategory category, Boolean vegetarian, Sorting sorting)
        {
            if((int)sorting == 3)
            {
                var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderByDescending(x => x.Name)
           .ToListAsync();
                return Alldish;
            }
            else if((int)sorting == 4)
            {
                var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderByDescending(x => x.Price)
           .ToListAsync();
                return Alldish;
            }
            else if((int)sorting == 5)
            {
                var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderByDescending(x => x.Rating)
           .ToListAsync();
                return Alldish;
            }
            return null;
        }
        public async Task<List<DishEntity>> SortingAsc(DishCategory category, Boolean vegetarian, Sorting sorting)
        {
            if ((int)sorting == 0)
            {
                var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderBy(x => x.Name)
           .ToListAsync();
                return Alldish;
            }
            else if ((int)sorting == 1)
            {
                var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderBy(x => x.Price)
           .ToListAsync();
                return Alldish;
            }
            else if ((int)sorting == 2)
            {
                var Alldish = await _context
           .Dish
           .Where(x => x.Category == category && x.Vegetarian == vegetarian)
           .OrderBy(x => x.Rating)
           .ToListAsync();
                return Alldish;
            }
            return null;
        }
    }
}
