using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Models.Domain.User
{
    public class User
    {
        public Guid ID { get; set; } 
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string TIN { get; set; } = string.Empty; 
        public bool AutomaticLogin { get; set; } 
        public TypeStatusUser Status { get; set; }
        public ShopProjectWebServer.Models.Domain.UserRole.UserRole UserRole { get; set; } = new UserRole.UserRole();
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
