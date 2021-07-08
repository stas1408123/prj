﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantMarket.Infrastructure.Services.CategoryService;
using PlantMarket.Common.Models;

namespace PlantMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllASync();
            
            if(categories == null)
            {
                return BadRequest();
            }

            return Ok(categories);
        }

        [HttpDelete]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var isDelete = await _categoryService.DeleteAsync(id);

            if(isDelete== false)
            {
                return BadRequest();
            }

            return Ok(isDelete);
        }


        [HttpPost]
        public async Task<ActionResult<Category>> AddNewCategory([FromBody] Category newCategory)
        {
            var category = await _categoryService
                .AddCategoryAsync(newCategory);

            if(category == null)
            {
                return BadRequest();
            }

            return Ok(category);
        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory([FromBody] Category newCategory)
        {
            var category = await _categoryService
                .UpdateAsync(newCategory);

            if(category == null)
            {
                return BadRequest();
            }

            return Ok(newCategory);

        }

        [HttpPost]
        [Route("GetAllPalantInCategory")]
        public async Task<ActionResult<List<Plant>>> GetPlantInCategory([FromBody] Category category)
        {

            var plants = await _categoryService
                .GetAllPalantInCategory(category);

            if(plants == null)
            {
                return BadRequest();
            }

            return Ok(plants.ToList());


    }
    }

    
}
