using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantMarket.Common;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Data;
using PlantMarket.Infrastructure.Services.OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantMarket.Infrastructure.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly PlantMarketContext _plantMarketContext;
        private readonly IOrderService _orderService;
        private readonly ILogger<UserService> _logger;
        public UserService(PlantMarketContext plantMarketContext,
            IOrderService orderService,
            ILogger<UserService> logger)
        {
            _plantMarketContext = plantMarketContext;
            _orderService = orderService;
            _logger = logger;

        }

        public async Task<User> AddNewUserAsync(User user)
        {
            if (user is null)
            {
                return null;
            }
            try
            {
                var exUser = await _plantMarketContext
                    .Users.AddAsync(user);

                _plantMarketContext.SaveChanges();

                return exUser.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(AddNewUserAsync),
                    $"Failed to add new user",
                    ex);

                return null;
            }
        }

        public async Task<bool> DeleteByIdAsync(int userId)
        {
            try
            {
                if (!_plantMarketContext.Users.Any(user => user.Id == userId))
                {
                    return false;
                }
                var exUser = await _plantMarketContext.Users
                        .FirstOrDefaultAsync(item => item.Id == userId);

                _plantMarketContext.Users
                    .Remove(exUser);

                await _plantMarketContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(DeleteByIdAsync),
                    $"Cannot delete user user id={userId}",
                    ex);

                return false;
            }



        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                var users = await _plantMarketContext.Users
                    .Include(user => user.Orders)
                    .ThenInclude(order => order.OrderedPlants)
                    .ToListAsync();


                return users;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(GetAllAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            try
            {
                var user = await _plantMarketContext.Users
                       .Include(user => user.Orders)
                           .ThenInclude(order => order.OrderedPlants)
                       .Include(user => user.shopCart)
                       .FirstOrDefaultAsync(user => user.Id == userId);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(GetUserById),
                    $"Cannot get user from database user id={userId}",
                    ex);

                return null;
            }
        }

        public async Task<User> UpdateAsync(User newUser)
        {
            if (newUser is null)
            {
                return null;
            }

            try
            {
                var exUser = await _plantMarketContext.Users
                    .FirstOrDefaultAsync(user => user.Id == newUser.Id);


                exUser.Name = newUser.Name;

                exUser.SerName = newUser.SerName;

                exUser.Adress = newUser.Adress;

                exUser.Phone = newUser.Phone;

                exUser.Email = newUser.Email;


                await _plantMarketContext.SaveChangesAsync();

                return exUser;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(UpdateAsync),
                    $"Cannot update user",
                    ex);

                return null;
            }


        }
    }
}
