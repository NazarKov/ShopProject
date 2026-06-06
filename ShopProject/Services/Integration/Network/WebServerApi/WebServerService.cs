using ShopProject.Services.Integration.Network.ShopProjectWebServerApi; 
using ShopProject.Services.Integration.Network.WebServerApi.Controller;
using ShopProject.Services.Integration.Network.WebServerApi.Exception;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi
{
    internal class WebServerService : IMainWebServerService
    {
        private HttpClient _httpClient;
        public IMainDataBaseFacade DataBase { get; private set; }
        public ISettingDataBaseController Settings {  get; private set; }   
        private ISettingWebServerService _settingsService { get; set; }
        public WebServerService(ISettingWebServerService settingWebServerService) 
        {
            _httpClient = new HttpClient();
            _settingsService = settingWebServerService;
            if(_settingsService.Url != null && _settingsService.Url!= string.Empty)
            {
                Settings = new SettingsController(_httpClient);

                if(_settingsService.Token != null)
                {
                    _httpClient.BaseAddress = new Uri(_settingsService.Url);
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _settingsService.Token);

                    DataBase = new MainDataBaseFacade(_httpClient); 
                }  
            }
        }   
        public async Task SetUrl(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
            DataBase = new MainDataBaseFacade(_httpClient);
            Settings = new SettingsController(_httpClient);
        } 
        public void SetToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
