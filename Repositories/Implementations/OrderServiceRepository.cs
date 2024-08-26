using FlamingForkAdmin.Helper.Utilities;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Interfaces;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
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
                    try
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        orderResponse = JsonSerializer.Deserialize<OrderResponseModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (orderResponse.Orders == null)
                        {
                            return [];
                        }
                        else
                        {
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
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        return [];
                    }
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

        #endregion AllOrdersFetcher

        #region PlacedOrdersFetcher

        public async Task<List<OrderModel>> GetPlacedOrders()
        {
            OrderResponseModel? orderResponse = new();
            ApiResponseMessageModel? errorResponse = new();

            try
            {
                // Fetches authentication token from secure storage.
                string token = await SecureStorageHandler.GetAuthenticationToken();
                _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = new Uri("http://" + _ServerAddress + "/order/placedOrders");
                var response = await _HttpClient.GetAsync(uri);

                // Tries to deserialize the response to List<OrderModel> in case of sucessful fetch.
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        orderResponse = JsonSerializer.Deserialize<OrderResponseModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (orderResponse.Orders == null)
                        {
                            return [];
                        }
                        else
                        {
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
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        return [];
                    }
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

        #endregion PlacedOrdersFetcher

        public async Task<List<OrderModel>> GetBeingPreparedOrders()
        {
            OrderResponseModel? orderResponse = new();
            ApiResponseMessageModel? errorResponse = new();

            try
            {
                // Fetches authentication token from secure storage.
                string token = await SecureStorageHandler.GetAuthenticationToken();
                _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = new Uri("http://" + _ServerAddress + "/order/beingPreparedOrders");
                var response = await _HttpClient.GetAsync(uri);

                // Tries to deserialize the response to List<OrderModel> in case of sucessful fetch.
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        orderResponse = JsonSerializer.Deserialize<OrderResponseModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (orderResponse.Orders == null)
                        {
                            return [];
                        }
                        else
                        {
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
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        return [];
                    }
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

        public async Task<List<OrderModel>> GetBeingDeliveredOrders()
        { return []; }

        public async Task<List<OrderModel>> GetDeliveredOrder()
        { return []; }

        public async Task<List<OrderModel>> GetCancelledOrders()
        { return []; }

        #region OrderStatusUpdater

        public async Task<string> UpdateOrderStatus(OrderModel order)
        {
            ApiResponseMessageModel? responseMessage = new ApiResponseMessageModel();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string token = await SecureStorageHandler.GetAuthenticationToken();
            _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonContent = JsonSerializer.Serialize<OrderModel>(order, options);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            // Tries to communicate with backend API endpoint.
            try
            {
                var uri = new Uri("http://" + _ServerAddress + "/order/changeOrderStatus");
                var response = await _HttpClient.PutAsync(uri, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                responseMessage = JsonSerializer.Deserialize<ApiResponseMessageModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return responseMessage.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion OrderStatusUpdater
    }
}
