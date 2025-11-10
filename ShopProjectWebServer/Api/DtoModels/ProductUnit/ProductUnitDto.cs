using ShopProjectDataBase.Helper;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.ProductUnit
{
    public class ProductUnitDto
    {
<<<<<<< HEAD
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("NameUnit")]
        public string NameUnit { get; set; } = string.Empty;
        [JsonPropertyName("ShortNameUnit")]
        public string ShortNameUnit { get; set; } = string.Empty;
        [JsonPropertyName("Number")]
        public int Number { get; set; } = 0;
        [JsonPropertyName("Status")]
=======
        public string NameUnit { get; set; } = string.Empty;
        /// <summary>
        /// Повна назва одиниці
        /// </summary>
        public string ShortNameUnit { get; set; } = string.Empty;
        /// <summary>
        /// номер одиниці
        /// </summary>
        public int Number { get; set; } = 0;
        /// <summary>
        /// вибрані одиниці користувачем
        /// </summary>
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        public int Status { get; set; }
    }
}
