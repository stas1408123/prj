using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Services.ShopCartService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantMarket.Infrastructure.Services.UserService;
using PlantMarket.Infrastructure.Services.OrderService;

namespace PlantMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopCartController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IShopCartService _shopCartService;

        private readonly IOrderService _orderService;


        public ShopCartController(IShopCartService shopCartService, IUserService userService, IOrderService orderService)
        {
            _shopCartService = shopCartService;
            _userService = userService;
            _orderService = orderService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ShopCart>>> GetAllCart()
        {
            var products = await _shopCartService
                .GetAllShopCartsAsync();

            if (products is null)
            {
                return BadRequest();
            }

            return Ok(products);
        }


        [Route("DeleteShopCartItem")]
        [HttpPost]
        public async Task<ActionResult<bool>> DeleteShopCartItemAsync([FromBody] ShopCartItem shopCartItem)
        {
            var isDelete = await _shopCartService
                .DeleteShopCartItemAsync(shopCartItem);

            if (isDelete == false)
            {
                return BadRequest();
            }

            return Ok(isDelete);

        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddNewPlantToCart([FromBody] ShopCartItem shopCartItem)
        {

            var IsAdd = await _shopCartService
                .AddNewShopCartItemAsync(shopCartItem);

            if (!IsAdd)
            {
                return BadRequest();
            }

            return Ok(IsAdd);
        }

        [Route("CreateCart")]
        [HttpPost]
        public async Task<ActionResult<ShopCart>> CreateShopCart([FromBody] User user)
        {

            var exUser = await _shopCartService
                .CreateShopCartAsync(user);

            if (exUser==null)
            {
                return BadRequest();
            }

            return Ok(exUser);
        }

        [Route("GetCartByUser")]
        [HttpPost]
        public async Task<ActionResult<ShopCart>> GetShopCartByUser([FromBody] User user)
        {

            var exCart = await _shopCartService
                .GetCartByUserAsync(user);

            if (exCart == null)
            {
                return BadRequest();
            }

            return Ok(exCart);
        }

        [HttpGet]
        [Route("GetShopCart")]
        public async Task<ActionResult<ShopCart>> GetShopCart()
        {
            var userIdString = User.FindFirst("userid")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest();
            }

            var user = await _userService
                .GetUserById(userId);

            var exCart = await _shopCartService
                .GetCartByUserAsync(user);

            if (exCart == null)
            {
                return BadRequest();
            }

            return Ok(exCart);
        }


        [HttpPost]
        [Route("AddPlantToCart")]
        public async Task<ActionResult<bool>> AddPlantToCart([FromBody] ShopCartItem shopCartItem)
        {
            var userIdString = User.FindFirst("userid")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest();
            }

            var user = await _userService
                .GetUserById(userId);

            shopCartItem.ShopCart = user.shopCart;

            var IsAdd = await _shopCartService
                .AddNewShopCartItemAsync(shopCartItem);

            if (!IsAdd)
            {
                return BadRequest();
            }

            return Ok(IsAdd);
        }

        [HttpPost]
        [Route("Buy")]
        public async Task<ActionResult<Order>> Buy([FromBody] ShopCart shopCar)
        {
            

            var result = await _shopCartService.BuyAsync(shopCar);


            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }


    }
}
