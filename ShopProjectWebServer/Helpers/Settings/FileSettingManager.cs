using System.Text.Json;

namespace ShopProjectWebServer.Helpers.Settings
{
    public class FileSettingManager
    {
        private readonly string _pathFileSettigs = "Data\\Settings.json";
        private readonly string _filePath;

        public FileSettingManager()
        {
            var baseDirectory = AppContext.BaseDirectory;

            _filePath = Path.Combine(baseDirectory, _pathFileSettigs);

            if (!File.Exists(_filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
                File.WriteAllText(_filePath, string.Empty);
            }
        }

        public Settings ReadJson()
        {
            var jsonContent = File.ReadAllText(_filePath);
            if (jsonContent == string.Empty)
            {
                return new Settings() { SettingConnect = null};
            }
            return JsonSerializer.Deserialize<Settings>(jsonContent);
        }

        public void SaveJson<T>(T jsonObject)
        {
            File.WriteAllText(_filePath, jsonObject.ToString());
        }
    }
}
