using Microsoft.VisualBasic.Devices;
using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.HomePage
{
    internal class StartModel
    {
        private NetworkURL _networkURL;

        public StartModel() 
        {
            _networkURL = new NetworkURL();
        }


        public async Task<string> GetUrl(string iprouter, int port, int minAddress, int maxAddress)
        {
            try
            { 
                _networkURL.IpRouter = iprouter;
                _networkURL.Port = port;
                _networkURL.MinIPAddress = minAddress;
                _networkURL.MaxIPAddress = maxAddress;

                NetworkScanner.Network = _networkURL;

                var URl = await NetworkScanner.SearchDataBaseURLAsync();

                _networkURL.Url = URl;
                return URl;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }
        public async Task<string> ConnectWebServer()
        {
            try 
            { 
                AppSettingsManager.SetParameterFile("URL", _networkURL.Serialize());
                MainWebServerController.Init(); 
                return await MainWebServerController.settings.Ping();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
