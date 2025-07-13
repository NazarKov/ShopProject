using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProjectDataBase.DataBase.Entities;

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

        public List<OperationsRecorderEntity> GetAllOperationsRecorderOperationsUser()
        {
            Task t = Task.Run(async () =>
            {
              //  _operationsRecordersUser = (await MainWebServerController.MainDataBaseConntroller.OperationRecorderAndUserController.GetOperationRecordersAndUser(Session.Token)).ToList();
            });
            t.Wait();

            var result = _operationsRecordersUser.Where(item => item.Users.ID == Session.User.ID).ToList();

            foreach (var item in result)
            {
                _operationsRecorders.Add(item.OpertionsRecorders);
            }
            return _operationsRecorders;
        }
    }
}
