using PlantMarket.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Infrastructure.Services.UserService
{
    public interface IUserService
    {

        Task<List<User>> GetAllAsync();
        Task<User> AddNewUserAsync(User user);

        Task<bool> DeleteByIdAsync(int userId);

        Task<User> UpdateAsync(User user);

        Task<User> GetUserById(int userId);


    }
}
