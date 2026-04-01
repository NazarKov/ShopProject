using ShopProject.Model.Domain.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.NetworkUrlScanner.Interface
{
    internal interface INetworkUrlManagerService
    {
        public Task<string> Search(string iprouter, int port, int minAddress, int maxAddress);
        public Task<bool> Ping(string url, string nameServer, bool saveSetting = true);

        public void SetSettingUrl(string url, string nameServer, string selectServer, bool saveSetting = true);
        public ServersSelectionSetting GetSettingUrl();
    }
}
