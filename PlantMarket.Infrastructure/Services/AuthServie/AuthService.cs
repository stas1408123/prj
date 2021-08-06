using Microsoft.EntityFrameworkCore;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Infrastructure.Services.UserService;
using PlantMarket.Infrastructure.Services.ShopCartService;
using Microsoft.Extensions.Logging;
using PlantMarket.Common;

namespace PlantMarket.Infrastructure.Services.AuthServie
{
    public class AuthService : IAuthService
    {

        private readonly PlantMarketContext _plantMarketContext;

        private readonly IUserService _userService;

        private readonly IShopCartService _shopCartService1;

        private readonly ILogger<AuthService> _logger;

        public AuthService(PlantMarketContext plantMarketContext,
            IUserService userService,
            IShopCartService shopCartService,
            ILogger<AuthService> logger)
        {
            _plantMarketContext = plantMarketContext;
            _userService = userService;
            _shopCartService1 = shopCartService;
            _logger = logger;

        }

        public async Task<bool> IsLoginFree(string login)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login))
            {
                return false;

            }

            try
            {
                string hashedLogin = GetHashedValue(login);

                var users = await _plantMarketContext.AccessDatas
                        .ToListAsync();

                var accessData = users
                    .FirstOrDefault(user => AreHashStringEquals(user.Login, hashedLogin));

                if (accessData == null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthServie),
                    nameof(IsLoginFree),
                    $"Failed checked login",
                    ex);

                return false;
            }


        }

        public async Task<User> LogIn(string login, string password)
        {
            try
            {

                string hashedLogin = GetHashedValue(login);

                var accessData = _plantMarketContext.AccessDatas
                    .AsEnumerable()
                    .FirstOrDefault(data => AreHashStringEquals(data.Login, hashedLogin));

                if (accessData != null)
                {
                    string hashedPassword = GetHashedValue(password);

                    if (AreHashStringEquals(hashedPassword, accessData.Password))
                    {
                        return await _userService.GetUserById(accessData.UserId);
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthServie),
                    nameof(LogIn),
                    $"Failed login",
                    ex);

                return null;
            }
        }

        public async Task Register(string login, string password, User user)
        {
            try
            {
                string hashedLogin = GetHashedValue(login);

                string hashedPassword = GetHashedValue(password);

                var accessData = new AccessData
                {
                    Login = hashedLogin,
                    Password = hashedPassword,
                    UserId = user.Id,
                    User = user
                };

                await _plantMarketContext.AccessDatas
                    .AddAsync(accessData);

                await _shopCartService1.CreateShopCartAsync(user);

                await _plantMarketContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthServie),
                    nameof(Register),
                    $"Failed register",
                    ex);
            }

        }



        private string GetHashedValue(string sourceString)
        {
            byte[] sourceStringByteArray;

            byte[] hashedByteArray;

            sourceStringByteArray = ASCIIEncoding.ASCII.GetBytes(sourceString);

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            hashedByteArray = md5Hasher.ComputeHash(sourceStringByteArray);

            return ByteArrayToString(hashedByteArray);
        }

        private bool AreHashStringEquals(string firstValue, string secondValue)
        {

            bool Equal = true;

            if (firstValue.Length == secondValue.Length)
            {
                for (int i = 0; i < firstValue.Length; i++)
                {
                    if (firstValue[i] != secondValue[i])
                    {
                        Equal = false;
                        break;
                    }
                }
            }
            else
            {
                Equal = false;
            }

            return Equal;
        }


        private string ByteArrayToString(byte[] inputValue)
        {
            StringBuilder result = new StringBuilder(inputValue.Length);

            for (int i = 0; i < inputValue.Length - 1; i++)
            {
                result.Append(inputValue[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
