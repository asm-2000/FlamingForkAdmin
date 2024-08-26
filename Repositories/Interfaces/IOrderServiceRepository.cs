using FlamingForkAdmin.Models;

namespace FlamingForkAdmin.Repositories.Interfaces
{
    public interface IOrderServiceRepository
    {
        Task<List<OrderModel>> GetAllOrders();
        Task<List<OrderModel>> GetPlacedOrders();
        Task<List<OrderModel>> GetBeingPreparedOrders();
        Task<List<OrderModel>> GetBeingDeliveredOrders();
        Task<List<OrderModel>> GetDeliveredOrders();
        Task<List<OrderModel>> GetCancelledOrders();
        Task<string> UpdateOrderStatus(OrderModel order);
    }
}
