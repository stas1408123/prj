using Microsoft.Extensions.DependencyInjection;
using PlantMarket.Infrastructure.Services.CategoryService;
using PlantMarket.Infrastructure.Services.PlantService;
using PlantMarket.Infrastructure.Services.UserService;
using PlantMarket.Infrastructure.Services.OrderService;
using PlantMarket.Infrastructure.Services.ShopCartService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantMarket.Infrastructure.Services.AuthServie;

namespace PlantMarket
{
    public static class DependencyContainer
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            services.AddTransient<IPlantService, PlantService>();
            
            services.AddTransient<ICategoryService, CategoryService>();
            
            services.AddTransient<IUserService, UserService>();
            
            services.AddTransient<IOrderService, OrderService>();
            
            services.AddTransient<IShopCartService, ShopCartService>();

            services.AddTransient<IAuthService, AuthService>();
        }
    }
}
