using ShopProjectWebServer.DataBase.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Helpers.Settings
{
    public class SettingDataBase
    { 
        /// <summary>
        /// Рядок підключення
        /// </summary>
        [JsonPropertyName("ConnectionString")]
        public string ConnectionString { get; set; }
        /// <summary>
        /// Тип бази даних
        [JsonPropertyName("TypeDataBase")]
        public TypeDataBase TypeDataBase { get; set; } = TypeDataBase.None; 

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
