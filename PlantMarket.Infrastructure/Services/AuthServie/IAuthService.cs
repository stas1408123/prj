using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Common.Models;

namespace PlantMarket.Infrastructure.Services.AuthServie
{
    public interface IAuthService
    {

        public Task Register(string login,string password,User user);

        public Task<bool> IsLoginFree(string login);

        public Task<User> LogIn( string login, string password);

    }
}
