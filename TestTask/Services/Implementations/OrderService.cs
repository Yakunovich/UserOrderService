using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public OrderService(ApplicationDbContext context)
        {
            Context = context;
        }
        private ApplicationDbContext Context { get; }

        public Task<Order> GetOrder()
        {
            return Context.Orders
                 .Where(order => order.Quantity > 1)
                 .OrderByDescending(order => order.CreatedAt)
                 .FirstOrDefaultAsync();
        }

        public Task<List<Order>> GetOrders()
        {
            return Context.Orders
                .Where(order => order.User.Status == UserStatus.Active)
                .OrderBy(order => order.CreatedAt)
                .ToListAsync();
        }
    }
}