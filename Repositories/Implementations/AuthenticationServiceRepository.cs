using System.Text.Json;
using System.Text;
using FlamingForkAdmin.Models;
using FlamingForkAdmin.Helper.Utilities;

namespace FlamingForkAdmin.Repositories.Implementations
{
    public class AuthenticationServiceRepository
    {
        private string _Address;
        private HttpClient _HttpClient;

        public AuthenticationServiceRepository()
        {
            _HttpClient = new HttpClient();
            _Address = "localhost:8080";
        }

        #region Login Handler API Service

        public async Task<string> LoginCustomer(AdminModel adminCredentials)
        {
            ApiResponseMessageModel? loginErrorResponse = new();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonContent = JsonSerializer.Serialize<AdminModel>(adminCredentials, options);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            // Commmnicates with API and returns received message.
            try
            {
                var uri = new Uri("http://" + _Address + "/user/loginAdmin");
                var response = await _HttpClient.PostAsync(uri, content);

                // Tries to deserialize the response to LoginResponseModel in case of sucessful login.
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    string? token = JsonSerializer.Deserialize<string>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    await SecureStorageHandler.StoreAuthenticationToken(token);
                    return "Sign In successful!";
                }
                // Deserializes the response to ApiResponseMessageModel in case of error status.
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    loginErrorResponse = JsonSerializer.Deserialize<ApiResponseMessageModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return loginErrorResponse.Message;
                }
            }
            // Returns exception message if communication with API fails.
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion Login Handler API Service
    }
}
