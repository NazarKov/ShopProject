using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorderUser
{
    public class BindingUserToOperationRecorderDto
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
    }
}
