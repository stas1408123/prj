using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantMarket.Common;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Infrastructure.Services.ShopCartService
{
    public class ShopCartService : IShopCartService
    {

        private readonly PlantMarketContext _plantMarketContext;
        private readonly ILogger<ShopCartService> _logger;

        public ShopCartService(PlantMarketContext plantMarketContext,
            ILogger<ShopCartService> logger)
        {
            _plantMarketContext = plantMarketContext;
            _logger = logger;
        }

        public async Task<bool> AddNewShopCartItemAsync(ShopCartItem shopCartItem)
        {
            if (shopCartItem is null || shopCartItem.Id != 0)
            {
                return false;
            }
            try
            {

                var exPlant = await _plantMarketContext.Plants
                     .FirstOrDefaultAsync(item => item.Id == shopCartItem.Plant.Id);

                _plantMarketContext.ShopCarItem
                    .Add(
                    new ShopCartItem
                    {
                        Plant = exPlant,
                        ShopCart = shopCartItem.ShopCart,
                    }
                    );

                await _plantMarketContext.SaveChangesAsync();


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ShopCartService),
                    nameof(AddNewShopCartItemAsync),
                    $"Failed to add new item to cart",
                    ex);

                return false;
            }
        }

        public async Task<ShopCart> CreateShopCartAsync(User user)
        {
            if (user is null)
            {
                return null;
            }
            try
            {
                var exUser = await _plantMarketContext.Users
                    .FirstOrDefaultAsync(us => us.Id == user.Id);

                var exShopCart = new ShopCart
                {
                    User = exUser,
                };

                exUser.shopCart = exShopCart;

                await _plantMarketContext.ShopCarts.AddAsync(exShopCart);


                await _plantMarketContext.SaveChangesAsync();

                return exShopCart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ShopCartService),
                    nameof(CreateShopCartAsync),
                    $"Failed creating cart",
                    ex);

                return null;
            }

        }

        public async Task<bool> DeleteShopCartItemAsync(ShopCartItem shopCartItem)
        {
            if (shopCartItem is null)
            {
                return false;
            }
            try
            {

                var exCartItem = await _plantMarketContext.ShopCarItem
                    .FirstOrDefaultAsync(item => item.Id == shopCartItem.Id);


                _plantMarketContext.ShopCarItem
                    .Remove(exCartItem);

                await _plantMarketContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ShopCartService),
                    nameof(DeleteShopCartItemAsync),
                    $"Cannot dalete ShopCart item shopcart id={shopCartItem.Id}",
                    ex);

                return false;
            }

        }


        public async Task<ShopCart> GetCartByUserAsync(User user)
        {
            if(user is null)
            {
                return null;
            }
            try
            {

                var exShopCart = await _plantMarketContext.ShopCarts
                    .Include(p => p.ShopItems)
                        .ThenInclude(plant => plant.Plant)
                    .Include(item => item.User)
                    .FirstOrDefaultAsync(item => item.UserId == user.Id);

                return exShopCart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ShopCartService),
                    nameof(GetCartByUserAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }

        }


        public async Task<List<ShopCart>> GetAllShopCartsAsync()
        {
            try
            {
                var shopCarts = await _plantMarketContext
                    .ShopCarts
                    .Include(item => item.ShopItems)
                        .ThenInclude(plant => plant.Plant)
                    .Include(item => item.User)
                    .ToListAsync();

                return shopCarts;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ShopCartService),
                    nameof(GetAllShopCartsAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public async Task<Order> BuyAsync(ShopCart shopCart)
        {
            if(shopCart is null)
            {
                return null;
            }
            try
            {

                var user = await _plantMarketContext.
                    Users.FirstOrDefaultAsync(user => user.Id == shopCart.UserId);

                var exShopCartItems = _plantMarketContext.ShopCarItem
                    .Include(shopCartItem => shopCartItem.Plant)
                    .Where(ShopCartItem => ShopCartItem.ShopCartId == shopCart.Id);

                var order = new Order()
                {
                    Name = user.Name,
                    SerName = user.SerName,
                    Adress = user.Adress,
                    Phone = user.Phone,
                    Email = user.Email,
                    User = user,
                    OrderedPlants = new List<OrderedPlant>()
                };

                foreach (ShopCartItem shopCartItem in exShopCartItems)
                {
                    _plantMarketContext.OrderedPlant
                        .Add(
                        new OrderedPlant
                        {
                            Plant = shopCartItem.Plant,
                            Order = order

                        });

                }

                await _plantMarketContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ShopCartService),
                    nameof(BuyAsync),
                    $"Failed operation Buy",
                    ex);

                return null;
            }

        }
    }
}
