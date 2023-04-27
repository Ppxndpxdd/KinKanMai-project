using Microsoft.AspNetCore.Mvc;

namespace KinKanMaiUI.Repositories
{
    public interface IReceivedRepository
    {
        Task<IEnumerable<Order>> GetOrder();
        Task<Order> GetOrderById(int orderId);

        Task<IEnumerable<Order>> UserReceived();

        Task<bool> UpdateOrders(int orderId, int newStatus = 2);
    }
}