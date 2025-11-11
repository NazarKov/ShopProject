using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.UserRole
{
    public class UserRoleDto 
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("NameRole")]
        public string NameRole { get; set; } = string.Empty;
        [JsonPropertyName("TypeAccess")]
        public int TypeAccess { get; set; } = 0;
    }
}
