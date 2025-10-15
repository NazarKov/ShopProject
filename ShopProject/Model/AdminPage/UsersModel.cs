 
using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.Template.Paginator; 
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 
using ShopProject.UIModel.UserPage;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProject.UIModel.OperationRecorderPage;

namespace ShopProject.Model.AdminPage
{ 
    internal class UsersModel
    {
        private static List<User> _users;
        private string? _token;
        public UsersModel() 
        {
            _users = new List<User>();
            _token = Session.User.Token;
        } 
        public async Task<PaginatorData<User>> GetUsersPageColumn(int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.UserController.GetUsersPageColumn(_token, page, countColumn, status);
                 
                PaginatorData<User> paginator = new PaginatorData<User>()
                {
                    Data = result.Data.ToUser(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<User>();
            }
        }

        public async Task<PaginatorData<User>> SearchByName(string item, int page, int countColumn, TypeStatusUser status )
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.UserController.GetUserByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new PaginatorData<User>()
                {
                    Data = result.Data.ToUser(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<User>();
            }
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            { 
                return await MainWebServerController.MainDataBaseConntroller.UserController.DeleteUser(_token,user.ID.ToString()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public async Task<List<OperationRecorderDialogWindow>> GetAllObject()
        {
            try
            {
                List<OperationRecorderDialogWindow> result = new List<OperationRecorderDialogWindow>();  
                var response = (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationRecorders(_token)).ToOperationRecorder().ToList(); 
                
                foreach (var item in response)
                {
                    result.Add(new OperationRecorderDialogWindow(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        public async Task<bool> SaveBinding(User user, List<OperationRecorderDialogWindow> objectOwnerHelpers)
        {
            try
            {
                List<OperationRecorder> result = new List<OperationRecorder>();
                foreach (var item in objectOwnerHelpers)
                {
                    if (item.isActive)
                    {
                        result.Add(item.deviceSettlementOperations);
                    }
                } 
                return await MainWebServerController.MainDataBaseConntroller.OperationRecorderAndUserController.AddOperationRecordersAndUser(_token, user.ID, result.ToOperationRecordersEntity());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        } 
    }
}
