using ShopProject.Helpers;
using ShopProject.Model.Exceptions;
using ShopProject.Services.Integration.File.Directory.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping; 
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task LoadSessionResourse()
        {
            var user = _sessionService.User;
            if (user == null) 
            {
                throw new AuthorizationException("Помилка авторизації");
            }
            var token = _sessionService.User.Token;
            if (token == null) 
            {
                throw new AuthorizationException("Помилка авторизації"); 
            }

            try
            {
                if (await _mainWebServerService.IsConnectServer())
                {
                    _sessionService.Roles = (await _mainWebServerService.DataBase.UserRoleController.GetRoles(token)).ToUserRole();
                    _sessionService.ProductUnits = (await _mainWebServerService.DataBase.ProductUnitController.GetUnits(token)).ToProductUnit();
                    _sessionService.ProductCodesUKTZED = (await _mainWebServerService.DataBase.ProductCodeUKTZEDController.GetCodeUKTZED(token)).ToProductCodeUKTZED();

                    _sessionService.User = await _mainWebServerService.DataBase.UserController.GetUser(token);
                    _sessionService.User.Token = token;
                    _sessionService.User.SignatureKey = (await _mainWebServerService.DataBase.SignatureKeyController.GetKey(token)).ToSignatureKey();

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
    }
}
