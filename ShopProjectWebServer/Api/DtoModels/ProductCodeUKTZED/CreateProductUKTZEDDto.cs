using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED
{
    public class CreateProductUKTZEDDto
    {
        public string NameCode { get; set; } = string.Empty;
        /// <summary>
        /// код УКТЗЕД
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// вибрані коди користувачем
        /// </summary>
        public TypeStatusCodeUKTZEDDto Status { get; set; }
    }
}
