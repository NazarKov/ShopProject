using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper
{
    public static class CheckingResponse
    {
        public static object Unpacking<T>(string responseBody)
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

        public static async  Task<object> UnpackingAsync<T>(string responseBody)
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
