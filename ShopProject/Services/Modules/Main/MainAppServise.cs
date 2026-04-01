using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface; 
using ShopProject.Services.Modules.Main.Interface;
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Main
{
    internal class MainAppServise : IMainAppServise
    {
        private ISettingService _settingService;
        private IMainWebServerService _mainWebServerService;
        private IResourseService _resourseSerivce;
        public MainAppServise(ISettingService settingService, IMainWebServerService mainWebServerService , IResourseService resourseSerivce)
        {
            _settingService = settingService;
            _mainWebServerService = mainWebServerService;
            _resourseSerivce = resourseSerivce;
        }

        public async Task<bool> IsConnectServer()
        {
            try
            { 
                return await _mainWebServerService.IsConnectServer();
            }
            catch (Exception)
            {
                return false;
            }
        } 
        public async Task LoadResourse() => await _resourseSerivce.LoadSessionResourse();


        public void Init()
        {
            //Resources.Init();
            //Resources.InitWebServerResourses();
        }
        public async Task<bool> IsConnectWebServer()
        {
            try
            {
                //await MainWebServerService.IsConnectServer();
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
            //var url = SettingService.GetSetting<NetworkURL>("URL"); 
            //NetworkScanner.Network = SettingService.GetSetting<NetworkURL>("URL");

            //var URlwebServer = await NetworkScanner.SearchDataBaseURLAsync();

            //url.Url = URlwebServer;
            //SettingService.SetSetting<NetworkURL>(url, "URL");
            //MainWebServerController.Init(url.Url);
        }
    }
}
