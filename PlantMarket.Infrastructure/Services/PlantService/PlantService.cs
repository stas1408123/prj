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

namespace PlantMarket.Infrastructure.Services.PlantService
{
    public class PlantService : IPlantService
    {
        private readonly PlantMarketContext _plantMarketContext;
        private readonly ILogger<PlantService> _logger;


        public PlantService(PlantMarketContext plantMarketContext,
            ILogger<PlantService> logger)
        {
            _logger = logger;
            _plantMarketContext = plantMarketContext;
        }

        public async Task<Plant> AddPlantAsync(Plant newPlant)
        {
            if(newPlant== null || newPlant.Id!=0)
            {
                return null;
            }
            try
            {

                await _plantMarketContext.Plants
                   .AddAsync(newPlant);

                await _plantMarketContext.SaveChangesAsync();

                return newPlant;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(AddPlantAsync),
                    $"Failed to add new plant",
                    ex);

                return null;
            }
        }

        public async Task<bool> DeleteAsync(int plantId)
        {
            try
            {
                if (!_plantMarketContext.Plants.Any(plant => plant.Id == plantId))
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
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(DeleteAsync),
                    $"Failed delete plant id={plantId}",
                    ex);

                return false;
            }

        }

        public async Task<List<Plant>> GetAllAsync()
        {
            try
            {
                var plants = await _plantMarketContext.Plants
                        .Include(item => item.Category)
                        .Where(item => item.IsAvailable == true)
                        .ToListAsync();

                return plants;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(GetAllAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public async Task<List<Plant>> GetFavPlants()
        {
            try
            {
                var plants = await _plantMarketContext.Plants
                        .Include(item => item.Category)
                        .Where(item => item.IsFavourite == true)
                        .ToListAsync();

                return plants;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(GetFavPlants),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public async Task<Plant> GetPlantByIdAsync(int plantId)
        {
            try
            {
                return await _plantMarketContext.Plants
                        .FirstOrDefaultAsync(item => item.Id == plantId);
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(GetPlantByIdAsync),
                    $"Cannot get plant from database plant id={plantId}",
                    ex);

                return null;
            }
        }

        public async Task<Plant> UpdateAsync(Plant plant)
        {
            if(plant is null)
            {
                return null;
            }

            try
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
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(UpdateAsync),
                    $"Failed updating plant id={plant.Id}",
                    ex);

                return null;
            }

        }

        public async Task<List<Plant>> GetAllPalantInCategory(Category category)
        {
            if (category == null)
            {
                return null;
            }

            try
            {

                var plants = _plantMarketContext.Plants
                    .Include(item => item.Category)
                    .Where(item => item.Category.Id == category.Id);

                return await plants.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(GetAllPalantInCategory),
                    $"Cannot get plants from database ",
                    ex);

                return null;
            }

        }

        public async Task<List<Plant>> Search(string name)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            try
            {
                var plants = _plantMarketContext.Plants
                    .Include(category => category.Category)
                    .Where(plant => plant.Name == name);

                return await plants.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(PlantService),
                    nameof(Search),
                    $"Cannot get plants from database",
                    ex);

                return null;
            }

        }
    }
}
