 
using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
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
            return _users;
        }

        public async Task<PaginatorData<UserEntity>> GetUsersPageColumn(int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.UserController.GetUsersPageColumn(Session.Token, page, countColumn, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<UserEntity>();
            }
        }

        public async Task<PaginatorData<UserEntity>> SearchByName(string item, int page, int countColumn, TypeStatusUser status )
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.UserController.GetUserByNamePageColumn(Session.Token, item, page, countColumn, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<UserEntity>();
            }
        }

        public async Task<bool> DeleteUser(UserEntity user)
        {
            try
            {
                user.SignatureKey = null;
                user.UserRole = null;
                return await MainWebServerController.MainDataBaseConntroller.UserController.DeleteUser(Session.Token,user); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

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
                List<OperationsRecorderEntity> result = new List<OperationsRecorderEntity>();
                foreach (var item in objectOwnerHelpers)
                {
                    if (item.isActive)
                    {
                        item.deviceSettlementOperations.MediaAccessControls = new List<MediaAccessControlEntity>();
                        result.Add(item.deviceSettlementOperations);
                    }
                }


                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = (await MainWebServerController.MainDataBaseConntroller.OperationRecorderAndUserController.AddOperationRecordersAndUser(Session.Token,user.ID, result));
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
