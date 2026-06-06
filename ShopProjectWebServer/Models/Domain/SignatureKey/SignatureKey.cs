using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Models.Domain.SignatureKey
{
    public class SignatureKey
    {
        public Guid ID { get; set; } 
        public byte[]? Signature { get; set; } 
        public string? SignaturePassword { get; set; }  
        public DateTimeOffset CreateAt { get; set; } 
        public DateTimeOffset EndAt { get; set; }
    }
}
