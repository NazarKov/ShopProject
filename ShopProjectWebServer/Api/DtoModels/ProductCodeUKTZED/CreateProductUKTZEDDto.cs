using ShopProjectDataBase.Helper;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED
{
    public class CreateProductUKTZEDDto
    {
        [JsonPropertyName("NameCode")]
        public string NameCode { get; set; } = string.Empty;
        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;
<<<<<<< HEAD
        [JsonPropertyName("Status")]
=======
        /// <summary>
        /// вибрані коди користувачем
        /// </summary>
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        public int Status { get; set; }
    }
}
