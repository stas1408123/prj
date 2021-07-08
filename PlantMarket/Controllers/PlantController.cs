using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Services.PlantService;

namespace PlantMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : Controller
    {
        private readonly IPlantService _productService;


        public PlantController(IPlantService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<List<Plant>>> GetAllProducts()
        {
            var products = await _productService
                .GetAllAsync();

            if (products is null)
            {
                return BadRequest();
            }

            return Ok(products);
        }
/*
        // GET: PlantController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlantController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlantController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
