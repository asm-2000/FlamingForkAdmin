using FlamingForkAdmin.Helper.Utilities;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FlamingForkAdmin.Repositories.Implementations
{
    public class OrderServiceRepository : IOrderServiceRepository
    {
        private HttpClient _HttpClient;
        private string _ServerAddress;

        public OrderServiceRepository()
        {
            _HttpClient = new HttpClient();
            _ServerAddress = "localhost:8080";
        }
        #region AllOrdersFetcher
        public async Task<List<OrderModel>> GetAllOrders()
        {
            OrderResponseModel? orderResponse = new();
            ApiResponseMessageModel? errorResponse = new();

            try
            {
                // Fetches authentication token from secure storage.
                string token = await SecureStorageHandler.GetAuthenticationToken();
                _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = new Uri("http://" + _ServerAddress + "/order/allOrders/");
                var response = await _HttpClient.GetAsync(uri);

                // Tries to deserialize the response to List<OrderModel> in case of sucessful fetch.
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    orderResponse = JsonSerializer.Deserialize<OrderResponseModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    foreach (OrderModel order in orderResponse.Orders)
                    {
                        int totalPrice = 0;
                        foreach (OrderItemModel orderItem in order.OrderItems)
                        {
                            totalPrice += orderItem.OrderItemPrice * orderItem.Quantity;
                        }
                        order.TotalPrice = totalPrice;
                    }

                    return orderResponse.Orders;
                }
                // Deserializes the response to ApiResponseMessageModel in case of error status.
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    errorResponse = JsonSerializer.Deserialize<ApiResponseMessageModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return [];
                }
            }
            // Returns empty list if communication with API fails.
            catch
            {
                return [];
            }
        }
        #endregion
        public async Task<List<OrderModel>> GetPlacedOrders() { return []; }
        public async Task<List<OrderModel>> GetBeingPreparedOrders() { return []; }
        public async Task<List<OrderModel>> GetBeingDeliveredOrders() { return []; }
        public async Task<List<OrderModel>> GetDeliveredOrder() { return []; }
        public async Task<List<OrderModel>> GetCancelledOrders() { return []; }
        public async Task<string> UpdateOrderStatus(OrderModel order) { return "sucess"; }

    }
}
