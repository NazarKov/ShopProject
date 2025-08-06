using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopProject.Model.HomePage
{
    internal class HomeModel
    {
        public HomeModel(){}

        public void Init()
        {
            Resources.Init();
        }
        public async Task<bool> IsConnectWebServer()
        {
            try
            {
                await MainWebServerController.IsConnectServer();
                return true;
            }
            catch (HttpRequestException)
            {
                await Reconnect();
                return true;
            }
            catch (TaskCanceledException)
            {
                await Reconnect();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task Reconnect()
        {
            var connecString = AppSettingsManager.GetParameterFiles("URL");
            var url = NetworkURL.Deserialize(connecString.ToString());
            NetworkScanner.Network = url;

            var URlwebServer = await NetworkScanner.SearchDataBaseURLAsync();

            url.Url = URlwebServer;
            AppSettingsManager.SetParameterFile("URL", url.Serialize());
            MainWebServerController.Init();
        }
    }
}
