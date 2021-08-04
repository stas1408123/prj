using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService
                .GetAllAsync();

            if(users == null)
            {
                return BadRequest();
            }

            return Ok(users);
        }



        [HttpPost]
        public async Task<ActionResult<User>> AddNewUser([FromBody] User newUser)
        {
            var user = await _userService
                .AddNewUserAsync(newUser);

            if(user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }


        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User newUser)
        {
            var user = await _userService
                .UpdateAsync(newUser);

            if(user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUses(int id)
        {
            var IsDelete = await _userService
                .DeleteByIdAsync(id);

            if(!IsDelete)
            {
                return BadRequest();
            }

            return Ok(IsDelete);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            var userIdString = User.FindFirst("userid")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest();
            }

            var user = await _userService
                .GetUserById(userId);

            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user);
        }



    }
}
