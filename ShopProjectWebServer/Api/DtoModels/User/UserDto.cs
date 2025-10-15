using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.User
{
    public class UserDto
    {
        public string Login { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty; 
        public string FullName { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string TIN { get; set; } = string.Empty; 
        public bool AutomaticLogin { get; set; } 
        public int Status { get; set; } 
        public int? UserRole_ID { get; set; } 
        public Guid? SignatureKey_ID { get; set; } 
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
