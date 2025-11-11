using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.UserRole
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
