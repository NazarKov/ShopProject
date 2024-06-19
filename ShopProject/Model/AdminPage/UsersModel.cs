using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.AdminPage
{ 
    internal class UsersModel
    {
        private IEntityAccess<UserEntiti> _userTable;
        private IEntityUpdate<UserEntiti> _userTableUpdate;
        private IEntityAccess<OperationsRecorderEntiti> _objectTable;
        private IEntityAccess<OperationsRecorderUserEntiti> _operationUserTable;


        public UsersModel() 
        {
            _userTable = new UserTableAccess();
            _userTableUpdate = new UserTableAccess();
            _objectTable = new OperationsRecorderTableAccess();
            _operationUserTable = new OperationsRecorderUserTableAccess();
        }
        public List<UserEntiti> GetUsers()
        {
            return (List<UserEntiti>)_userTable.GetAll();
        }
        public bool DeleteUser(UserEntiti user)
        {
            try
            {
                _userTable.Delete(user);

                return true;
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
                foreach (var item in _objectTable.GetAll())
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
        public bool SaveBinding(UserEntiti user, List<SoftwareDeviceSettlementOperationsHelper> objectOwnerHelpers)
        {
            try
            {
                foreach(var item in objectOwnerHelpers)
                {
                    if (item.isActive)
                    {
                        _operationUserTable.Add(new OperationsRecorderUserEntiti()
                        {
                            OpertionsRecorders = item.deviceSettlementOperations,
                            Users = user,
                        });
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public List<UserEntiti> SearchObject(string item)
        {
            try
            {
                var items = _userTableUpdate.GetAll();
                if (items != null)
                {
                    if (item != " ")
                    {
                        return items.Where(i => i.FullName.ToLower().Contains(item.ToLower())).ToList();
                    }
                }
                return new List<UserEntiti>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<UserEntiti>();
            }
        }
    }
}
