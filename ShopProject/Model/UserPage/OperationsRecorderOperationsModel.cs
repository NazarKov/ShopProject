using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProjectSQLDataBase.Entities;
using System.Windows;

namespace ShopProject.Model.UserPage
{
    internal class OperationsRecorderOperationsModel
    {
        private List<OperationsRecorderUserEntity> _operationsRecordersUser;
        private List<OperationsRecorderEntity> _operationsRecorders;

        public OperationsRecorderOperationsModel()
        {
            _operationsRecordersUser = new List<OperationsRecorderUserEntity>();
            _operationsRecorders = new List<OperationsRecorderEntity>();
        }

        public async Task<List<OperationsRecorderEntity>> GetAllOperationsRecorderOperationsUser()
        {
            try
            {
                _operationsRecordersUser = (await MainWebServerController.MainDataBaseConntroller.OperationRecorderAndUserController.GetOperationRecordersAndUser(Session.Token)).ToList();


                var result = _operationsRecordersUser.Where(item => item.Users.ID == Session.User.ID).ToList();

                foreach (var item in result)
                {
                    if (item.OpertionsRecorders != null)
                    {
                        _operationsRecorders.Add(item.OpertionsRecorders);
                    }
                }
                return _operationsRecorders;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return new List<OperationsRecorderEntity>();
            }
        }

        public async Task<List<OperationsRecorderEntity>> Search(string item)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationRecordersByNumberAndUser(Session.Token,item,Session.User.ID)).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<OperationsRecorderEntity>();
            } 
        }
    }
}
