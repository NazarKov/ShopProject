using ShopProjectWebServer.DataBase.Helpers;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Models.Domain.Setting
{
    public class SettingDataBaseConnection
    {
        [JsonPropertyName("ConnectionString")]
        public ConnectionString ConnectionString { get; set; } = new ConnectionString();

        [JsonPropertyName("TypeDataBase")]
        public TypeDataBase TypeDataBase { get; set; } = TypeDataBase.None;
    }
}
