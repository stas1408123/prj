using PlantMarket.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PlantMarket.Infrastructure.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly PlantMarketContext _plantMarketContext;
        public CategoryService(PlantMarketContext plantMarketContext)
        {
            _plantMarketContext = plantMarketContext;
        }

        public async Task<Category> AddCategoryAsync(Category newCategory)
        {
            
            if (newCategory != null)
            {
                await _plantMarketContext.AddAsync<Category>(newCategory);
                await _plantMarketContext.SaveChangesAsync();
                return newCategory;
            }

            return null;

        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            if (categoryId == 0)
            {
                return false;
            }

            var exCategory = await _plantMarketContext.Categories
                .Include(item => item.Plants)
                .FirstOrDefaultAsync(i => i.Id == categoryId);

            _plantMarketContext.Categories
                .Remove(exCategory);

            await _plantMarketContext.SaveChangesAsync();

            return true;

        }

        public async Task<List<Category>> GetAllASync()
        {
            var categories = await _plantMarketContext.Categories
                .Include(item => item.Plants)
                .ToListAsync();

            return categories;

        }



        public async Task<Category> UpdateAsync(Category category)
        {
            if(category == null)
            {
                return null;
            }
            var exCategory = await _plantMarketContext.Categories
                .Include(item => item.Plants)
                .FirstOrDefaultAsync(item  => item.Id== category.Id);

            exCategory.Name = category.Name;
            exCategory.Description = category.Description;
            exCategory.Plants = category.Plants;

            await _plantMarketContext.SaveChangesAsync();

            return exCategory;
           
        }

        
        public async Task<List<Plant>> GetAllPalantInCategory(Category category)
        {
            if (category == null)
            {
                return null;
            }

            var plants = _plantMarketContext.Plants
                .Include(item => item.Category)
                .Where(item => item.Category.Name == category.Name);

            return await plants.ToListAsync();
           
        }
    }
}
