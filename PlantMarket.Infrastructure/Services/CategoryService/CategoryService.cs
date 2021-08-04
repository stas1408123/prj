using PlantMarket.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantMarket.Common;

namespace PlantMarket.Infrastructure.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly PlantMarketContext _plantMarketContext;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(PlantMarketContext plantMarketContext,
            ILogger<CategoryService> logger)
        {
            _plantMarketContext = plantMarketContext;
            _logger = logger;
        }

        public async Task<Category> AddCategoryAsync(Category newCategory)
        {
            if(newCategory is null)
            {
                return null;
            }

            try
            {
                await _plantMarketContext.AddAsync<Category>(newCategory);
                await _plantMarketContext.SaveChangesAsync();
                return newCategory;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CategoryService),
                    nameof(AddCategoryAsync),
                    $"Failed to add new category",
                    ex);

                return null;
            }

        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            if (categoryId == 0)
            {
                return false;
            }

            try
            {
                var exCategory = await _plantMarketContext.Categories
                    .Include(item => item.Plants)
                    .FirstOrDefaultAsync(i => i.Id == categoryId);

                _plantMarketContext.Categories
                    .Remove(exCategory);

                await _plantMarketContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CategoryService),
                    nameof(DeleteAsync),
                    $"Failed delete category",
                    ex);

                return false;
            }

        }

        public async Task<List<Category>> GetAllASync()
        {
            try
            {

                var categories = await _plantMarketContext.Categories
                    .Include(item => item.Plants)
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CategoryService),
                    nameof(GetAllASync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }


        }



        public async Task<Category> UpdateAsync(Category category)
        {
            if(category == null)
            {
                return null;
            }

            try
            {
                var exCategory = await _plantMarketContext.Categories
                    .Include(item => item.Plants)
                    .FirstOrDefaultAsync(item => item.Id == category.Id);

                exCategory.Name = category.Name;
                exCategory.Description = category.Description;


                if (exCategory.Plants.Count != 0)
                {
                    _plantMarketContext.Plants
                        .RemoveRange(exCategory.Plants);
                }

                exCategory.Plants.AddRange(category.Plants);

                await _plantMarketContext.SaveChangesAsync();

                return exCategory;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CategoryService),
                    nameof(UpdateAsync),
                    $"Failed updating category id={category.Id}",
                    ex);

                return null;
            }

        }

        
        
    }
}
