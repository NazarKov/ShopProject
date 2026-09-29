using ShopProject.Model.Domain.User;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Main.Interface;
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;  
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Main
{
    internal class MainAppServise : IMainAppServise
    { 
        private readonly IMainWebServerService _mainWebServerService;
        private readonly IResourseService _resourseSerivce;
        private readonly ISessionService _sessionService;
        private readonly ISettingService _settingService;
        private readonly IUserService _userService;
        public MainAppServise(ISettingService settingService, IMainWebServerService mainWebServerService , IResourseService resourseSerivce, 
            ISessionService sessionService,IUserService userService)
        { 
            _mainWebServerService = mainWebServerService;
            _resourseSerivce = resourseSerivce;
            _sessionService = sessionService;
            _settingService = settingService;
            _userService = userService;
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

        public async Task LoadSession()
        {
            var result = await _resourseSerivce.LoadWorkingShift();
            if (result.IsSuccess)
            {
                _sessionService.WorkingShiftStatus = result.Data;
                _settingService.SetSetting<WorkingShiftStatus>(result.Data);
            }
        } 
        public async Task LoadStartData() => await _resourseSerivce.LoadStartData();
        public async Task<OperationResult<User>> LoadUserData() => await _userService.GetCurrentUserFromServer();
    }
}
