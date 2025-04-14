using NPOI.SS.Formula.Functions;
using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZXing;

namespace ShopProject.Model.AdminPage
{ 
    internal class UsersModel
    {
        private static List<UserEntity> _users;
        public UsersModel() 
        {
            _users = new List<UserEntity>();
        }
        public List<UserEntity> GetUsers()
        {
            Task t = Task.Run(async () =>
            {
                _users = (await MainWebServerController.MainDataBaseConntroller.UserController.GetUsers(Session.Token)).ToList();
            });
            t.Wait();
            return _users;
        }
        //public bool DeleteUser(UserEntiti user)
        //{
        //    try
        //    {
        //        _userTable.Delete(user);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }

        //}
        public List<SoftwareDeviceSettlementOperationsHelper> GetAllObject()
        {
            try
            {
                List<SoftwareDeviceSettlementOperationsHelper> result = new List<SoftwareDeviceSettlementOperationsHelper>();
                List<OperationsRecorderEntity> response = new List<OperationsRecorderEntity>();
                Task t = Task.Run(async () =>
                {
                    response = (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationRecorders(Session.Token)).ToList();
                });
                t.Wait();
                foreach (var item in response)
                {
                    result.Add(new SoftwareDeviceSettlementOperationsHelper(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        public bool SaveBinding(UserEntity user, List<SoftwareDeviceSettlementOperationsHelper> objectOwnerHelpers)
        {
            try
            {
                List<OperationsRecorderUserEntity> result = new List<OperationsRecorderUserEntity>();
                foreach (var item in objectOwnerHelpers)
                {
                    if (item.isActive)
                    {
                        result.Add(new OperationsRecorderUserEntity()
                        {
                            OpertionsRecorders = item.deviceSettlementOperations,
                            Users = user,
                        });
                    }
                }


                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = (await MainWebServerController.MainDataBaseConntroller.OperationRecorderAndUserController.AddOperationRecordersAndUser(Session.Token, result));
                });
                t.Wait();
                return response;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        //public List<UserEntiti> SearchObject(string item)
        //{
        //    try
        //    {
        //        var items = _userTableUpdate.GetAll();
        //        if (items != null)
        //        {
        //            if (item != " ")
        //            {
        //                return items.Where(i => i.FullName.ToLower().Contains(item.ToLower())).ToList();
        //            }
        //        }
        //        return new List<UserEntiti>();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return new List<UserEntiti>();
        //    }
        //}
    }
}
