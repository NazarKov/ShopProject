using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer
{
    public class ControlWebServerDto
    {
        [JsonPropertyName("IsEnabled")]
        public bool IsEnabled { get; set; }
        [JsonPropertyName("IsEnableDataBase")]
        public bool IsEnableDataBase { get; set; }
    }
}
