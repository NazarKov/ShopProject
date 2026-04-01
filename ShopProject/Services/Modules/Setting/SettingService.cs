using ShopProject.Services.Integration.File.Directory.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System.Collections.Generic;
using System.Text.Json;

namespace ShopProject.Services.Modules.Setting
{
    internal class SettingService : ISettingService
    { 
        private readonly string _pathSetting;
        private Dictionary<string, object> _cache;
        private IFileServise _fileServise;

        public SettingService(IDirectoryService directoryService, IFileServise fileServise)
        {
            _fileServise = fileServise;
            directoryService.Init();
            _pathSetting = directoryService.GetPathSetting(); 
            _pathSetting += "\\setting.json";  

            _cache= new Dictionary<string, object>();
        } 

        public TSetting GetSetting<TSetting>()
        {
            EnsureLoaded();

            if (_cache.TryGetValue(typeof(TSetting).Name, out var value))
            {
                if (value is TSetting typed)
                    return typed;
                 
                if (value is object str)
                    return JsonSerializer.Deserialize<TSetting>(str.ToString());
            }

            return default;
        }

        public void SetSetting<TSetting>(TSetting value)
        {
            EnsureLoaded();

            _cache[typeof(TSetting).Name] = value;

            var json = JsonSerializer.Serialize(_cache, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            _fileServise.Write(_pathSetting, json);
        }
        private void EnsureLoaded()
        {
            if (_cache != null&& _cache.Count != 0) return; 

            var json =_fileServise.Read(_pathSetting);
            if(json == null)
            {
                return; 
            }

            _cache = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        } 
    }
}
