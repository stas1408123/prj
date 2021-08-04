using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Common.Models;

namespace PlantMarket.Infrastructure.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllASync();

        Task<Category> AddCategoryAsync(Category newCategory);

        Task<Category> UpdateAsync(Category category);

        Task<bool> DeleteAsync(int categoryId);


    }
}
