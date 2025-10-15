using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common
{
    public static class ChekingResponse
    {
        public static object Unpacking<T>(string responseBody)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var response = JsonSerializer.Deserialize<ApiResponse<T>>(responseBody, options);
            if (response != null)
            {
                switch (response.Status)
                {
                    case ResponseStatus.None:
                        {
                            return "OK";
                            break;
                        }
                    case ResponseStatus.Message:
                        {
                            return JsonSerializer.Deserialize<T>(response.Message, options);
                        }
                    case ResponseStatus.Error:
                        {
                            throw new Exception(response.Errors.ToString());
                            break;
                        }
                    default:
                        {
                            return null;
                            break;
                        }
                }
            }
            else
            {
                throw new Exception("Серевер не доступний");
            }
        }

        public static async Task<object> UnpackingAsync<T>(string responseBody)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var response = JsonSerializer.Deserialize<ResponseWebServer>(responseBody, options);
            if (response != null)
            {
                switch (response.Type)
                {
                    case ResponseWebServerType.None:
                        {
                            return "OK";
                            break;
                        }
                    case ResponseWebServerType.Message:
                        {
                            return JsonSerializer.Deserialize<T>(response.MessageBody, options);
                        }
                    case ResponseWebServerType.Error:
                        {
                            throw new Exception(response.MessageBody);
                            break;
                        }
                    default:
                        {
                            return null;
                            break;
                        }
                }
            }
            else
            {
                throw new Exception("Серевер не доступний");
            }
        }
    }
}
