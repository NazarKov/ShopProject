using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Exceptions;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface; 
using ShopProject.Services.Modules.NetworkUrlManager;
using ShopProject.Services.Modules.NetworkUrlScanner.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.NetworkUrlScanner
{
    internal class NetworkUrlManagerService : INetworkUrlManagerService
    {
        private NetworkURL _networkURL;
        private ISettingService _settingService;
        private IMainWebServerService _webServerService;

        public NetworkUrlManagerService(ISettingService settingService , IMainWebServerService mainWebServerService)
        {
            _settingService = settingService;
            _webServerService = mainWebServerService;
            _networkURL = new NetworkURL();
        }
        public async Task<string> Search(string iprouter, int port, int minAddress, int maxAddress)
        {
            if (iprouter == string.Empty)
            {
                throw new ExceptionStringEmpty("Не вказанuй iprouter");
            }

            if (port == 0)
            {
                throw new ExceptionStringEmpty("Не вказаний port");
            }

            if (minAddress == 0)
            {
                throw new ExceptionStringEmpty("Не вказаний minAddress");
            }

            if (maxAddress == 0)
            {
                throw new ExceptionStringEmpty("Не вказаний maxAddress");
            }

            string result = string.Empty;
            AggregateExceptionUrl aggregateException = null;

            Task t = Task.Run(async () => {
                try
                {  
                    _networkURL.IpRouter = iprouter;
                    _networkURL.Port = port;
                    _networkURL.MinIPAddress = minAddress;
                    _networkURL.MaxIPAddress = maxAddress;

                    NetworkScanner.Network = _networkURL;

                    result = await NetworkScanner.SearchDataBaseURLAsync();   
                }
                catch (Exception ex)
                {
                    aggregateException = new AggregateExceptionUrl(ex.Message);
                } 
            });
            await t.ContinueWith(t =>
            {
                if(aggregateException != null)
                {
                    throw aggregateException;
                }
                return result;
            }); 
            return result; 
        }

        public async Task<bool> Ping(string url,string nameServer , bool saveSetting = true)
        {

            if (nameServer == string.Empty)
            {
                throw new ExceptionStringEmpty("Не вказана назва серверу");
            }

            if (url == string.Empty)
            {
                throw new ExceptionStringEmpty("Не вказаний Url");
            } 

            try
            {
                await _webServerService.SetUrl(url);
                var result = await _webServerService.IsConnectServer();
                var time = DateTime.Now;

                if (result)
                {
                    SetSettingUrl(url, nameServer, nameServer + " : " + url, saveSetting);
                } 
                return result;
            } 
            catch
            {
                throw;
            }
        }
        public void SetSettingUrl(string url, string nameServer , string selectServer , bool saveSetting = true)
        { 
            var setting = _settingService.GetSetting<ServersSelectionSetting>();

            if(setting == null)
            {
                setting = new ServersSelectionSetting();
            }


            if (saveSetting)
            {
                if(setting.Servers.Count < 5)
                {
                    setting.Servers.Add(new ServerSelectionSetting() {  NameServer = nameServer, UrlServer = url });
                }
                else
                {
                    setting.Servers.RemoveAt(0);
                    setting.Servers.Add(new ServerSelectionSetting() { NameServer = nameServer, UrlServer = url });
                }
            }

            setting.SelectServer = selectServer;  
            _settingService.SetSetting<ServersSelectionSetting>(setting); 
        }
        public ServersSelectionSetting GetSettingUrl()
        { 
            var setting = _settingService.GetSetting<ServersSelectionSetting>();
            if(setting == null)
            {
                return new ServersSelectionSetting();
            }
            return setting;
        }
    }
}
