using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Order
{
    public class OrderDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("Count")]
        public int Count { get; set; } = 0;
        [JsonPropertyName("ProductID")]
        public string ProductID { get; set; }
        [JsonPropertyName("OperationID")]
        public int OperationID { get; set; }
    }
}
