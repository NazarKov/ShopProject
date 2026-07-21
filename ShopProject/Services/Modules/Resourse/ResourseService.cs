using Azure;
using ShopProject.Helpers;
using ShopProject.Model.Exceptions;
using ShopProject.Services.Integration.Directory.Interface;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Mapping.ProductCodeUKTZED;
using ShopProject.Services.Modules.Mapping.ProductUnit;
using ShopProject.Services.Modules.Mapping.SignatureKey;
using ShopProject.Services.Modules.Mapping.User;
using ShopProject.Services.Modules.Mapping.UserRole;
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Aztec.Internal;

namespace ShopProject.Services.Modules.Resourse
{
    internal class ResourseService : IResourseService
    {
        private IDirectoryService _directoryService;
        private ISessionService _sessionService;
        private IMainWebServerService _mainWebServerService;
        public ResourseService(IDirectoryService directoryService, ISessionService sessionService,IMainWebServerService mainWebServerService)
        {
            _directoryService = directoryService;
            _sessionService = sessionService; 
            _mainWebServerService = mainWebServerService;
        } 
        public bool IsInitSystemFolders()
        {
            _directoryService.Init();
            if (_directoryService.IsCreateProgramFolders())
            {
                return true;
            }
            else
            { 
                _directoryService.CreateProgramFolders();
                return true;
            }
        }
        public async Task LoadStartData()
        {
            var response = await _mainWebServerService.DataBase.BootStrapController.GetStartData();
            if ((ResultStatus)response.Status == ResultStatus.Success)
            {
                _sessionService.Roles = response.Data.Roles.ToUserRole();
                _sessionService.ProductCodesUKTZED = response.Data.ProductCodeUKTZEDs.ToProductCodeUKTZED();
                _sessionService.ProductUnits = response.Data.ProductUnits.ToProductUnit();
            }
            else
            {
                throw new Exception("Невдалося заватажити ресурси");
            }
        } 

        public async Task LoadUserData()
        {
            var response = await _mainWebServerService.DataBase.UserController.GetUser(_sessionService.User.Token);
            if((ResultStatus)response.Status == ResultStatus.Success)
            {
                _sessionService.User = response.Data.ToUser(_sessionService.Roles.ToUserRoleDto());
            }
            else
            {
                throw new Exception("Невдалося заватажити ресурси");
            }
        }




    }
}
