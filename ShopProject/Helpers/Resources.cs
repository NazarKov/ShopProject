using Microsoft.EntityFrameworkCore.Query.Internal;
using ShopProject.Helpers.Exceptions;
using ShopProject.Helpers.NetworkServise;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.UserPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopProject.Helpers
{
    public static class Resources
    {
        public static void Init()
        {
            InitWebServer();
            InitSystemFolders();
        }

        private static void InitSystemFolders()
        {
            FileDirectory.Init();
            if (!FileDirectory.IsCreateProgramFolders())
            {
                FileDirectory.CreateProgramFolders();
            }
        }
        private static void InitWebServer()
        {
            var networkURL = AppSettingsManager.GetParameterFiles("URL").ToString();
            if (networkURL != null && networkURL != string.Empty)
            {
                MainWebServerController.Init(NetworkURL.Deserialize(networkURL).Url); 
            }
            else
            {
                throw new ExceptionURL();
            }
        }
        public static void InitWebServerResourses()
        {
            if (Session.User!=null && Session.User.Token != null && Session.Roles == null) 
            {
                Task t = Task.Run(async () => {
                    var roles = await MainWebServerController.MainDataBaseConntroller.UserRoleController.GetRoles(Session.User.Token);
                    Session.Roles = roles.ToUserRole();
                    
                    var units = await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnits(Session.User.Token);
                    Session.ProductUnits = units.ToProductUnit();

                    var codesUKTZED = await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZED(Session.User.Token);
                    Session.ProductCodesUKTZED = codesUKTZED.ToProductCodeUKTZED();

                    var user = await MainWebServerController.MainDataBaseConntroller.UserController.GetUser(Session.User.Token);
                    var token = Session.User.Token;
                    Session.User = user;
                    Session.User.Token = token;
                    var signatureKey = await MainWebServerController.MainDataBaseConntroller.SignatureKeyController.GetKey(Session.User.Token.ToString());
                    Session.User.SignatureKey = signatureKey.ToSignatureKey();
                });
            }
        }
    }
}
