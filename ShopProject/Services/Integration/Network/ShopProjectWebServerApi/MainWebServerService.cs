using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Controller;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Exception;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi
{
    internal class MainWebServerService : IMainWebServerService
    {  
        public IMainDataBaseFacade DataBase { get; private set; }
        private SettingsController Settings {  get; set; }  

        private ISettingWebServerService _settingsService { get; set; }
        public MainWebServerService(ISettingWebServerService settingWebServerService) 
        {
            _settingsService = settingWebServerService;
            if(_settingsService.Url != null && _settingsService.Url!= string.Empty)
            {
                DataBase = new MainDataBaseFacade(_settingsService.Url);
                Settings = new SettingsController(_settingsService.Url);
            }
        }   
        public async Task SetUrl(string url)
        {
            DataBase = new MainDataBaseFacade(url);
            Settings = new SettingsController(url);
        } 
        public async Task<bool> IsConnectServer()
        {
            try
            {
                var result = await Settings.Ping();
                var time = DateTime.Now;

                if (DateTime.TryParse(result, out time))
                { 
                    return true;
                }
                return false;
            }
            catch (TaskCanceledException)
            {
                throw new СonnectionException("Невдалося підключитися до сервера");
            }
        }
    }
}
