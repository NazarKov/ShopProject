using ShopProjectWebServer.DataBase.HelperModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Helpers.Settings
{
    public class SettingDataBase
    {
        /// <summary>
        /// Назва бази даних
        /// </summary>
        [JsonPropertyName("NameDataBase")]
        public string Name { get; set; }
        /// <summary>
        /// Рядок підключення
        /// </summary>
        [JsonPropertyName("ConnectionString")]
        public ConnectionString ConnectionString { get; set; }
        /// <summary>
        /// Тип бази даних
        [JsonPropertyName("TypeDataBase")]
        public TypeDataBase TypeDataBase { get; set; } = TypeDataBase.None;
        /// <summary>
        /// Пароль бази даних
        [JsonPropertyName("PasswordDataBase")]
        public string PasswordDataBase { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
