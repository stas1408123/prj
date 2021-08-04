using PlantMarket.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Infrastructure.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();

        Task<Order> GetOrderByIdAsync(int orderId);

        Task<Order> AddOrderAsync(Order newOrder);

        Task<bool> DeleteAsync(int orderId);

        Task<Order> UpdateAsync(Order order);

        Task<List<Order>> GetAllUserAsync(User user);

    }
}
