using System; 
using System.Net.Http; 
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Common
{
    public class SendHttpRequest
    {
        public static async Task<ApiResponse<T>> SendAsync<T>(Func<Task<HttpResponseMessage>> request)
        {
            try
            {
                using var response = await request();
                var body = await response.Content.ReadAsStringAsync();
                var code = response.StatusCode;

                if (response.IsSuccessStatusCode)
                    return ApiResponse<T>.Unpacking(body);
                else if(code == System.Net.HttpStatusCode.Unauthorized)
                {
                   return ApiResponse<T>.Fail("Невдалося авторизуватися",3);
                }

                return ApiResponse<T>.Fail("Невдалося отримати відповідь від сервера");
            }
            catch (HttpRequestException ex) { return ApiResponse<T>.Fail(ex.Message); }
            catch (TaskCanceledException) { return ApiResponse<T>.Fail("Час очікування вичерпано"); }
        }
    }
}
