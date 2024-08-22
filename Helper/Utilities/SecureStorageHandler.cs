using System.Text.Json;

namespace FlamingForkAdmin.Helper.Utilities
{
    public static class SecureStorageHandler
    {
        #region Token Storage Handler

        public static async Task StoreAuthenticationToken(string token)
        {
            string? authToken = token;
            //Store info in secure storage.
            await SecureStorage.SetAsync("Token", authToken);
        }

        #endregion Token Storage Handler

        #region Token Returner

        public static async Task<string> GetAuthenticationToken()
        {
            // Sees if the key-value pair exists.
            try
            {
                string? token = await SecureStorage.GetAsync("Token");
                if (string.IsNullOrEmpty(token))
                {
                    return "Not Found";
                }
                else
                {
                    return token;
                }
            }
            catch
            {
                return "Not Found";
            }
        }

        #endregion Token Returner

        #region Storage Clearer

        public static void ClearStorage()
        {
            // Removes authentication token from Secure Storage.
            SecureStorage.RemoveAll();
        }

        #endregion Storage Clearer
    }
}
