using KinKanMaiUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace KinKanMaiUI.Repositories
{
    public class ReceivedRepository : IReceivedRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public ReceivedRepository(ApplicationDbContext db,UserManager<IdentityUser> userManager,
             IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Shop>> Shops()
        {
            return await _db.Shops.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrder()
        {
            
            var orders = await _db.Orders
                            .Include(x => x.OrderStatus)
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Menu)
                            .ThenInclude(x => x.Shop)
                            .ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _db.Orders.FindAsync(orderId);
        }

        public async Task<bool> UpdateOrders(int orderId, int newStatus = 2)
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var order = await GetOrderById(orderId);

            if (order == null)
            {
                return false;
            }

            order.OrderStatusId = newStatus;
            order.ReceivedUserId = userId;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> UserReceived()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            var orders = await _db.Orders
                            .Include(x => x.OrderStatus)
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Menu)
                            .ThenInclude(x => x.Shop)
                            .Where(a => a.ReceivedUserId == userId)
                            .ToListAsync();
            return orders;
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
