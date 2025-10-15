using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.User
{
    public class UserDto
    { 
        public string ID { get; set; }
        public string Login { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty; 
        public string FullName { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string TIN { get; set; } = string.Empty; 
        public bool AutomaticLogin { get; set; } 
        public int Status { get; set; } 
        public int? UserRoleID { get; set; } 
        public Guid? SignatureKeyID { get; set; } 
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
