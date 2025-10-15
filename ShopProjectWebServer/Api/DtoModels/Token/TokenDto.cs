using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Api.DtoModels.Token
{
    public class TokenDto
    {
        public Guid? User_ID { get; set; } 
        public string Token { get; set; } = string.Empty; 
        public string Device { get; set; } = string.Empty; 
        public DateTime CreateAt { get; set; }
    }
}
