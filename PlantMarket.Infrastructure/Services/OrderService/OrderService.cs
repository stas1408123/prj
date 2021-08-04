using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantMarket.Common;
using PlantMarket.Common.Models;
using PlantMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Infrastructure.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly PlantMarketContext _plantMarketContext;
        private readonly ILogger<OrderService> _logger;

        public OrderService(PlantMarketContext plantMarketContext,
            ILogger<OrderService> logger)
        {
            _plantMarketContext = plantMarketContext;
            _logger = logger;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            try
            {

                var orders = await _plantMarketContext.Orders
                    .Include(item => item.OrderedPlants)
                    .ThenInclude(plant => plant.Plant)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GetAllAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }

        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            try
            {

                var order = await _plantMarketContext.Orders
                    .Include(item => item.OrderedPlants)
                    .ThenInclude(plant => plant.Plant)
                    .FirstOrDefaultAsync(item => item.Id == orderId);

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GetOrderByIdAsync),
                    $"Cannot get order id={orderId} from database",
                    ex);

                return null;
            }

        }

        public async Task<Order> AddOrderAsync(Order newOrder)
        {
            if (newOrder is null || newOrder.Id != 0)
            {
                return null;
            }

            try
            {
                _plantMarketContext.Orders
                    .Add(newOrder);

                await _plantMarketContext.SaveChangesAsync();


                var exOrder = await _plantMarketContext.Orders
                    .FirstOrDefaultAsync(item => item.CreationDate == newOrder.CreationDate
                        && item.UserId == newOrder.UserId);

                return exOrder;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(AddOrderAsync),
                    $"Failed add order to database",
                    ex);

                return null;
            }
        }

        public async Task<bool> DeleteAsync(int orderId)
        {
            if (orderId == 0)
            {
                return false;
            }
            try
            {

                var exOrder = await _plantMarketContext.Orders
                        .Include(item => item.OrderedPlants)
                        .ThenInclude(plant => plant.Plant)
                        .FirstOrDefaultAsync(item => item.Id == orderId);

                exOrder.OrderedPlants = new List<OrderedPlant>();

                await _plantMarketContext.SaveChangesAsync();

                _plantMarketContext.Orders
                    .Remove(exOrder);

                await _plantMarketContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(DeleteAsync),
                    $"Cannot delete order id ={orderId}",
                    ex);

                return false;
            }

        }

        public async Task<Order> UpdateAsync(Order order)
        {
            if (order is null)
            {
                return null;
            }
            try
            {
                var exOrder = await _plantMarketContext.Orders
                        .Include(item => item.OrderedPlants)
                         .ThenInclude(plant => plant.Plant)
                        .FirstOrDefaultAsync(item => item.Id == order.Id);

                if (exOrder != null)
                {
                    exOrder.UserId = order.UserId;

                    if (exOrder.OrderedPlants.Count != 0)
                    {
                        _plantMarketContext.OrderedPlant
                            .RemoveRange(exOrder.OrderedPlants);
                    }

                    exOrder.OrderedPlants.AddRange(order.OrderedPlants);
                }

                await _plantMarketContext.SaveChangesAsync();

                return exOrder;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(UpdateAsync),
                    $"Cannot update order",
                    ex);

                return null;
            }

        }

        public async Task<List<Order>> GetAllUserAsync(User user)
        {
            if (user is null)
            {
                return null;
            }
            try
            {
                var orders = await _plantMarketContext.Orders
                    .Include(item => item.OrderedPlants)
                    .ThenInclude(plant => plant.Plant)
                    .Where(order => order.UserId == user.Id)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GetAllAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }
    }
}
