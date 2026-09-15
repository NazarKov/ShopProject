using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Main.Interface;
using ShopProject.Services.Modules.Resourse.Interface; 
using ShopProject.Services.Modules.Setting.Interface;
using System; 
using System.Net.Http; 
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
        public async Task LoadStartData() => await _resourseSerivce.LoadStartData();
        public async Task LoadUserData() => await _resourseSerivce.LoadUserData(); 
    }
}
