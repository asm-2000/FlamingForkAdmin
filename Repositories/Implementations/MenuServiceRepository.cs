using FlamingForkAdmin.Helper.Utilities;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Repositories.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FlamingForkAdmin.Repositories.Implementations
{
    public class MenuServiceRepository : IMenuServiceRepository
    {
        private HttpClient _HttpClient;
        private string _ServerAddress;
        public MenuServiceRepository() 
        {
            _HttpClient = new HttpClient();
            _ServerAddress = "localhost:8080";
        }
        #region MenuItemAdder

        public async Task<string> AddMenuItem(MenuItemModel menuItem)
        {
            ApiResponseMessageModel? apiResponse = new();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            try
            {
                // Fetches authentication token from secure storage.
                string token = await SecureStorageHandler.GetAuthenticationToken();
                _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonContent = JsonSerializer.Serialize<MenuItemModel>(menuItem,options);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var uri = new Uri("http://" + _ServerAddress + "/menu/addMenuItem");
                var response = await _HttpClient.PostAsync(uri,content);

                // Tries to deserialize the response to ApiResponseMessageModel in case of sucessful communication with API.

                        string responseBody = await response.Content.ReadAsStringAsync();
                        apiResponse = JsonSerializer.Deserialize<ApiResponseMessageModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return apiResponse.Message;
            }
            // Returns empty list if communication with API fails.
            catch
            {
                return "Cannot Add Item!";
            }
        }

        #endregion MenuItemAdder

        #region MenuItemFetcher

        public async Task<List<MenuItemModel>> GetAllMenuItems()
        {
            MenuResponseModel? menuResponse = new MenuResponseModel();
            ApiResponseMessageModel? errorResponse = new ApiResponseMessageModel();
            try
            {
                // Fetches authentication token from secure storage.
                string token = await SecureStorageHandler.GetAuthenticationToken();
                _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = new Uri("http://" + _ServerAddress + "/menu/allMenuItems");
                var response = await _HttpClient.GetAsync(uri);

                // Tries to deserialize the response to MenuResponseModel in case of sucessful fetch.
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        menuResponse = JsonSerializer.Deserialize<MenuResponseModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (menuResponse.AllMenuItems == null)
                        {
                            return [];
                        }
                        else
                        {
                            return menuResponse.AllMenuItems;
                        }
                    }
                    catch (Exception ex)
                    {
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

        #endregion MenuItemFetcher

        #region MenuItemRemover

        public async Task<string> DeleteMenuItem(int itemId)
        {
            return string.Empty;
        }

        #endregion MenuItemRemover

        #region MenuItemUpdateService

        public async Task<string> UpdateMenuItemDetails(MenuItemModel menuItem)
        {
            return string.Empty;
        }

        #endregion MenuItemUpdateService
    }
}
