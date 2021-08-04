using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Services.AuthServie;
using PlantMarket.Infrastructure.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlantMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpGet]
        [Route("IsLoginFree/{login}")]
        public async Task<ActionResult<bool>> IsLoginFree(string login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            return await _authService
                .IsLoginFree(login);
        }


        [HttpPut]
        public async Task<ActionResult<bool>> LogInAsync([FromBody] AccessData data)
        {
            if (data.Login == null
                && data.Password == null)
            {
                return Ok(false);
            }

            var user = await _authService.LogIn(
                    data.Login,
                    data.Password);

            if (user == null)
            {
                return false;
            }

            await Authenticate(
                user.Id.ToString());

            return Ok(true);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<bool>> RegisterAsync([FromBody] RegisterData data)
        {
            if (data.Login == null
                || data.Password == null
                || data.User == null)
            {
                return Ok(false);
            }


            var user = await _userService
                .AddNewUserAsync(data.User);

            await _authService.Register(
                data.Login,
                data.Password,
                user);

            await Authenticate(
                user.Id.ToString());

            return Ok(true);

        }

        private async Task Authenticate(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(
                    "userid",
                    userId),
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }


        [Route("Logout")]
        [HttpGet]
        public async Task<ActionResult<bool>> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(true);

        }

    }
}
