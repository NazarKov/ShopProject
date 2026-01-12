using ShopProject.Helpers; 
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows;

namespace ShopProject.Model.UserPage
{
    internal class OperationsRecorderOperationsModel
    {  

        private readonly string _token;
        public OperationsRecorderOperationsModel()
        { 
            _token = Session.User.Token;
        }

        public async Task<List<OperationRecorder>> GetAllOperationsRecorderOperationsUser()
        {
            try
            {
                var item = (await MainWebServerController.MainDataBaseConntroller.OperationRecorderAndUserController.GetOperationRecordersAndUser(_token));


                var result = item.OpertionsRecorders.ToOperationRecorder(); 
                return result.ToList();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return new List<OperationRecorder>();
            }
        }

        public async Task<List<ObjectOwner>> GetObjectOwners()
        {
            try
            {
                var item = (await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.GetObjectsOwners(_token));

                var result = item.ToObjectOwner();
                return result.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
                return new List<ObjectOwner>();
            }
        }

        public async Task<List<OperationRecorder>> Search(string item)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationRecordersByNumberAndUser(_token, item,Session.User.ID)).ToOperationRecorder().ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<OperationRecorder>();
            } 
        }
    }
}
