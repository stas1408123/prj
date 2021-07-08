using Microsoft.EntityFrameworkCore;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Infrastructure.Services.PlantService
{
    public class PlantService : IPlantService
    {
        private readonly PlantMarketContext _plantMarketContext;


        public PlantService(PlantMarketContext plantMarketContext)
        {
            _plantMarketContext = plantMarketContext;
        }

        public async Task<Plant> AddPlantAsync(Plant newPlant)
        {

             await _plantMarketContext.Plants
                .AddAsync(newPlant);

             await _plantMarketContext.SaveChangesAsync();

            return newPlant;
        }

        public async Task<bool> DeleteAsync(int plantId)
        {
            if(!_plantMarketContext.Plants.Any(plant => plant.Id == plantId))
            {
                return false;
            }
            var exProduct = await _plantMarketContext.Plants
                    .FirstOrDefaultAsync(item => item.Id == plantId);

            _plantMarketContext.Plants
                .Remove(exProduct);

            await _plantMarketContext.SaveChangesAsync();

            return true;
            
        }

        public async Task<List<Plant>> GetAllAsync()
        {
            var plants = await _plantMarketContext.Plants
                    .Include(item => item.Category)
                    .Where(item => item.IsAvailable==true)
                    .ToListAsync();

            return plants;
        }

        public async Task<List<Plant>> GetFavPlants()
        {
            var plants = await _plantMarketContext.Plants
                    .Include(item => item.Category)
                    .Where(item => item.IsFavourite == true)
                    .ToListAsync();

            return plants; 
        }

        public async Task<Plant> GetPlantByIdAsync(int plantId)
        {
            return await _plantMarketContext.Plants
                    .FirstOrDefaultAsync(item => item.Id == plantId);
        }

        public async Task<Plant> UpdateAsync(Plant plant)
        {
            var exPlant = await _plantMarketContext.Plants
                    .FirstOrDefaultAsync(item => item.Id == plant.Id);

            if (!(exPlant is null))
            {
                exPlant.Name = plant.Name;
                exPlant.ShortDescription = plant.ShortDescription;
                exPlant.LongDescription = plant.LongDescription;
                exPlant.Price = plant.Price;
                exPlant.IsFavourite = plant.IsFavourite;
                exPlant.IsAvailable = plant.IsAvailable;
                exPlant.CategoryId = plant.CategoryId;
                exPlant.PictureLink = plant.PictureLink;
            }

            await _plantMarketContext.SaveChangesAsync();

            return exPlant;
        }
    }
}
