using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Interface
{
    internal interface IMainWebServerService
    { 
        public IMainDataBaseFacade DataBase {  get;}
        public ISettingDataBaseController Settings { get;}
        public Task<bool> IsConnectServer();
        public Task SetUrl(string url);
    }
}
