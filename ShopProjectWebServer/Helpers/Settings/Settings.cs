using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Helpers.Settings
{
    public class Settings
    {
        [JsonPropertyName("DefaultDataBase")]
        public SettingDataBase SettingConnect { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
