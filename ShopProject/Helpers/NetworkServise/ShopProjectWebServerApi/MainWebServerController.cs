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
        public static SettingsController settings;
        
        public static void Init()

        {
            _url = AppSettingsManager.GetParameterFiles("URL").ToString();
            //прописати пошук URl якшо йоно нема в налаштуваннях

            MainDataBaseConntroller = new MainDataBaseController(_url);
            settings = new SettingsController(_url);
        }


    }
}
