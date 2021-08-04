using PlantMarket.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Infrastructure.Services.ShopCartService
{
    public interface IShopCartService
    {

        Task<bool> AddNewShopCartItemAsync(ShopCartItem shopCartItem);

        Task<bool> DeleteShopCartItemAsync(ShopCartItem shopCartItem);

        Task<ShopCart> CreateShopCartAsync(User user);

        public Task<ShopCart> GetCartByUserAsync(User user);

        public Task<List<ShopCart>> GetAllShopCartsAsync();

        Task<Order> BuyAsync(ShopCart shopCar);


    }
}
