using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Common.Models;

namespace PlantMarket.Infrastructure.Services.PlantService
{
    public interface IPlantService
    {
        Task<List<Plant>> GetAllAsync();

        Task<Plant> GetPlantByIdAsync(int plantId);

        Task<List<Plant>> GetFavPlants();

        Task<Plant> AddPlantAsync(Plant newPlant);

        Task<Plant> UpdateAsync(Plant plant);

        Task<bool> DeleteAsync(int plantId);

        Task<List<Plant>> GetAllPalantInCategory(Category category);

        Task<List<Plant>> Search(string name);
    }
}
