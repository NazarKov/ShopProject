using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.HttpService;
using ShopProject.Helpers.HttpService.Model;
using ShopProject.Helpers.SigningFileService;
using ShopProject.Helpers.SigningFileService.Model;
using ShopProject.Views.SettingPage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.AdminPage
{
    class CreateUserModel
    {
        private IEntityAccess<UserEntiti> _userTable;
        private IEntityAccess<UserRoleEntiti> _userRoleTable;
        private IEntityAccess<ObjectOwnerEntiti> _objectOwnerTable;

        private HttpController _mainControllerHttp;
        private SigningFileContoller _mainControllerTcp;

        private List<UserRoleEntiti> _userRoles;
        private List<ObjectOwnerEntiti> _userObjects;

        public CreateUserModel()
        {
            _userTable = new UserTableAccess();
            _userRoleTable = new UserRoleTableAccess();
            _objectOwnerTable = new ObjectOwnerTableAccess();

            _userRoles = new List<UserRoleEntiti>();
            _userObjects = new List<ObjectOwnerEntiti>();

            _mainControllerHttp = new HttpController();
            _mainControllerTcp = new SigningFileContoller();

            new Thread(new ThreadStart(GetInfoDataBase)).Start();
        }
        private void GetInfoDataBase()
        {
            _userObjects = (List<ObjectOwnerEntiti>)_objectOwnerTable.GetAll();
        }
        public void CreateUser(string fulleName,string login ,string email, string password , string roles)
        {
            try
            {
                _userTable.Add(new UserEntiti()
                {
                    FullName = fulleName,
                    Login = login,
                    Email = email,
                    Password = password,
                    UserRole = _userRoles.Where(item => item.NameRole == roles).FirstOrDefault(),
                    Status = 0,
                    CreatedAt = DateTime.Now,

                });
                MessageBox.Show("Користувача створено");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public async Task<bool> CreateUserKey(string pathFile,string NameFile, string login ,string email, string password , string passwrodKey , string roles)
        {
            UserCommand result = new UserCommand() { Status = "100"};
            try
            {
                string namefile = Path.GetFileName("C:\\ProgramData\\ShopProject\\Temp\\Key.txt.p7s");


                //if (namefile == null && namefile ==string.Empty && namefile == "" )
                //{
                    if (!_mainControllerTcp.IsConnectingServise())
                    {
                        if (!_mainControllerTcp.IsStartServise())
                        {
                            _mainControllerTcp.StartServise();
                        }
                        _mainControllerTcp.ConnectService();
                    }

                     result = _mainControllerTcp.SendingCommand(new UserCommand()
                    {
                        TypeCommand = TypeCommand.IsInitialize,
                        Time = DateTime.Now,
                    });

                    if (result.Status == "404")
                    {
                        _mainControllerTcp.SendingCommand(new UserCommand()
                        {
                            TypeCommand = TypeCommand.Initialize,
                            Time = DateTime.Now,
                        });
                    }


                    result = _mainControllerTcp.SendingCommand(new UserCommand()
                    {
                        TypeCommand = TypeCommand.GetDataKey,
                        PathKey = pathFile,
                        PasswordKey = passwrodKey,
                        Time = DateTime.Now,
                    });
                //}

                if (result.Status == "100")
                {
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _mainControllerHttp.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;
                    if (nameUser != null)
                    {
                        FileDirectory.Init();
                        FileDirectory.CreateUserFolder(nameUser);
                        string pathKey = FileDirectory.CopyKeyInUserFolder(NameFile, pathFile, nameUser);

                        _userTable.Add(new UserEntiti()
                        {
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            KeyPassword = passwrodKey,
                            KeyPath = pathKey,
                            Status = 1,
                            CreatedAt = DateTime.Now,
                            UserRole = _userRoles.Where(item => item.NameRole == roles).FirstOrDefault()
                        });


                        _mainControllerTcp.SendingCommand(new UserCommand()
                        {
                            TypeCommand = TypeCommand.DisconnectUser,
                            Time = DateTime.Now,
                        });
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<string> GetUserRoles() 
        {
            try
            {
                List<string> nameRoles = new List<string>();
                _userRoles = (List<UserRoleEntiti>)_userRoleTable.GetAll();
                foreach(var item in _userRoles)
                {
                    nameRoles.Add(item.NameRole);
                }
                return nameRoles;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return new List<string>();
            }
        }
    }
}
