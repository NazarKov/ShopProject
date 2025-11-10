using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Api.DtoModels.Token
{
    public class TokenDto
    {
<<<<<<< HEAD
        public string? UserID { get; set; } 
=======
        public Guid? User_ID { get; set; } 
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        public string Token { get; set; } = string.Empty; 
        public string Device { get; set; } = string.Empty; 
        public DateTime CreateAt { get; set; }
    }
}
