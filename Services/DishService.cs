using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication2.DAL.Enums;
using static Azure.Core.HttpHeader;
using Azure;
using WebApplication2.DAL.Repository;

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
        private readonly IDishRepository _dishRepository;
        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task AddDish(DishDto model)
        {
            var dish = new DishEntity
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Image = model.Image,
                Vegetarian = model.Vegetarian,
                Rating = 0,
                Category = model.Category
            };
            await _dishRepository.AddDish(dish);
        }
        public async Task<DishDto> GetInfoDish(Guid id)
        {
            var dishEntity = await _dishRepository.GetDish(id.ToString());

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
                return await _dishRepository.SortingDescByName(category, vegetarian);
            }
            else if((int)sorting == 4)
            {
                return await _dishRepository.SortingDescByPrice(category, vegetarian);
            }
            else if((int)sorting == 5)
            {
                return await _dishRepository.SortingDescByRating(category, vegetarian);
            }
            return null;
        }
        public async Task<List<DishEntity>> SortingAsc(DishCategory category, Boolean vegetarian, Sorting sorting)
        {
            if ((int)sorting == 0)
            {
                return await _dishRepository.SortingAscByName(category, vegetarian);
            }
            else if ((int)sorting == 1)
            {
                return await _dishRepository.SortingAscByPrice(category, vegetarian);
            }
            else if ((int)sorting == 2)
            {
                return await _dishRepository.SortingAscByRating(category, vegetarian);
            }
            return null;
        }
    }
}
