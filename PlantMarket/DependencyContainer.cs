using Microsoft.Extensions.DependencyInjection;
using PlantMarket.Infrastructure.Services.CategoryService;
using PlantMarket.Infrastructure.Services.PlantService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantMarket
{
    public static class DependencyContainer
    {
        public static void RegisterDependency(this IServiceCollection services)
        {

            services.AddTransient<IPlantService, PlantService>();
            services.AddTransient<ICategoryService, CategoryService>();

        }
    }
}
