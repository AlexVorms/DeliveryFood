using Microsoft.AspNetCore.Mvc;
using WebApplication2.DAL.Entities;
using WebApplication2.DAL.Models;
using WebApplication2.DAL.Repository;

namespace WebApplication2.Services
{
    public interface IRatingService
    {
        Task<Int32> AddRating(string IdUser, Guid IdDish, double Rating);
        Task<Boolean> ChecksRating(string UserId, string DishId);
    }
    public class RatingServise: IRatingService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IOrderRepository _orderRepository;
        public RatingServise(IUserRepository userRepository, IRatingRepository ratingRepository, IDishRepository dishRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _ratingRepository = ratingRepository;
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
        }
        private async Task Ratings(string id, DishEntity dish)
        {
            var ratingList = await _ratingRepository.GetListUsersRatingsForDish(id);

            double rating = 0;
            int number = 0;
            foreach (var i in ratingList)
            {
                rating += i.Rating;
                number += 1;
            }
            rating = rating / number;

            await _ratingRepository.UpdateDishRating(dish.Id.ToString(), rating);
        }
        public async Task<Int32> AddRating(string IdUser, Guid IdDish, double Rating)
        {
            var userEntity = await _userRepository.GetUser(IdUser);

            var dishEntity = await _dishRepository.GetDish(IdDish.ToString());

            var checks = await ChecksRating(IdUser, IdDish.ToString());

            if (checks)
            {
                return 0;
            }
            else
            {
                var checksOrder = await ChecksOrder(IdUser, IdDish.ToString());
                if (checksOrder)
                {
                    var model = new RatingEntity
                    {
                        Id = Guid.NewGuid(),
                        Dish = dishEntity,
                        User = userEntity,
                        Rating = Rating
                    };
                    await _ratingRepository.AddUserRating(model);
                    await Ratings(IdDish.ToString(), dishEntity);
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }

        public async Task<Boolean> ChecksRating(string UserId,string DishId)
        {
            var rating = await _ratingRepository.GetUserRatingForDish(UserId, DishId);

            if(rating == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<Boolean> ChecksOrder(string UserId, string DishId)
        {
            var flag = false;
            var ListOrders = await _orderRepository.GetOrderListWithBasket(UserId);

            foreach (var order in ListOrders)
            {
                var ListDish = order.Basket;
                foreach(var dish in ListDish)
                {
                    if(dish.DishId == DishId)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
    }
}
