using ShopProjectDataBase.Helper;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED
{
    public class ProductCodeUKTZEDDto
    {
<<<<<<< HEAD
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("NameCode")]
        public string NameCode { get; set; } = string.Empty;
        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("Status")]
=======
        public string NameCode { get; set; } = string.Empty;
        /// <summary>
        /// код УКТЗЕД
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// вибрані коди користувачем
        /// </summary>
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        public int Status { get; set; }
    }
}
