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
        private static string _url;
        internal static MainDataBaseController MainDataBaseConntroller { get; set; }
        internal static SettingsController settings;
        
        public static void Init()
        {
            var networkURL = AppSettingsManager.GetParameterFiles("URL").ToString();

            _url = NetworkURL.Deserialize(networkURL).Url; 

            MainDataBaseConntroller = new MainDataBaseController(_url);
            settings = new SettingsController(_url); 
        }

        public static async Task<string> IsConnectServer()
        { 
            return await settings.Ping();
        }


    }
}
