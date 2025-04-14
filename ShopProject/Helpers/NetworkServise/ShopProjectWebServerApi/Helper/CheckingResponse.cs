using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper
{
    public static class CheckingResponse
    {
        public static object Unpacking<T>(string responseBody)
        {
            var response = JsonSerializer.Deserialize<ResponseWebServer>(responseBody);
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
                            return JsonSerializer.Deserialize<T>(response.MessageBody);
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
