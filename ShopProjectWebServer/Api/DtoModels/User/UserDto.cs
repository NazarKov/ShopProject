using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.User
{
    public class UserDto
<<<<<<< HEAD
    { 
        public string ID { get; set; }
=======
    {
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        public string Login { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty; 
        public string FullName { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string TIN { get; set; } = string.Empty; 
        public bool AutomaticLogin { get; set; } 
        public int Status { get; set; } 
<<<<<<< HEAD
        public int? UserRoleID { get; set; } 
        public Guid? SignatureKeyID { get; set; } 
=======
        public int? UserRole_ID { get; set; } 
        public Guid? SignatureKey_ID { get; set; } 
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
