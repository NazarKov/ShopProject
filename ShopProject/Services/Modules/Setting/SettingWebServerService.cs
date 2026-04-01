using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Setting
{
    internal class SettingWebServerService : ISettingWebServerService
    {
        private string _url = string.Empty; 
        public string Url { get { return _url; } private set { _url = value; }}

        private ISettingService _settingService;

        public SettingWebServerService(ISettingService setting)
        {
            _settingService = setting;
            var settingserver = _settingService.GetSetting<ServersSelectionSetting>();
            if(settingserver != null&&settingserver.SelectServer!= null && settingserver.SelectServer != string.Empty)
            {
                Url = settingserver.SelectServer.Split(" : ")[1];
            }
        }
    }
}
