using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Discount;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Operation
{
    public class OperationІnformationDto
    {
        [JsonPropertyName("Operation")]
        public OperationDto? Operation { get; set; }
        [JsonPropertyName("Discount")]
        public DiscountDto? Discount { get; set; }
        [JsonPropertyName("Products")]
        public IEnumerable<ProductDto>? Products { get; set; }
    }
}
