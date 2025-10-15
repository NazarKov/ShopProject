using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi
{
    public static class MainWebServerController
    { 
        internal static MainDataBaseController MainDataBaseConntroller { get; set; }
        internal static SettingsController settings;
        
        public static void Init(string url)
        {
            MainDataBaseConntroller = new MainDataBaseController(url);
            settings = new SettingsController(url); 
        }

        public static async Task<string> IsConnectServer()
        { 
            return await settings.Ping();
        }   
    }
}
