using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.ProductUnit
{
    public class ProductUnitDto
    {
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
        public int Status { get; set; }
    }
}
