using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        public UserService(ApplicationDbContext context)
        {
            Context = context;
        }
        private ApplicationDbContext Context { get; }
        public Task<User> GetUser()
        {
            return Context.Users
                 .OrderByDescending(user => user.Orders
                                                .Where(order => order.CreatedAt.Year == 2003 && order.Status == OrderStatus.Delivered)
                                                .Select(order => order.Quantity * order.Price))
                 .FirstOrDefaultAsync();
                                        
        }

        public Task<List<User>> GetUsers()
        {
            return Context.Users
                .Where(user => user.Orders
                                   .Where(order =>order.CreatedAt.Year == 2010 && order.Status == OrderStatus.Paid)
                                   .Any())
                .ToListAsync();
        }


    }
}
