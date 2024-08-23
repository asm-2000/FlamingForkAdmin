using FlamingForkAdmin.Models;

namespace FlamingForkAdmin.Repositories.Interfaces
{
    public interface IOrderServiceRepository
    {
        Task<OrderResponseModel> GetAllOrders();
        Task<OrderResponseModel> GetPlacedOrders();
        Task<OrderResponseModel> GetBeingPreparedOrders();
        Task<OrderResponseModel> GetBeingDeliveredOrders();
        Task<OrderResponseModel> GetDeliveredOrder();
        Task<OrderResponseModel> GetCancelledOrders();
        Task<ApiResponseMessageModel> UpdateOrderStatus(OrderModel order);
    }
}
