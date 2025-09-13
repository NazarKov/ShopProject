using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;  
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Collections.Generic; 
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ShopProjectSQLDataBase.Entities;
using ShopProject.UIModel;
using ShopProject.UIModel.SalePage;

namespace ShopProject.Helpers
{
    public class Session
    {
        public static UserEntity User { get; set; }
        public static string Token { get; set; }

        public static ProductEntity Product { get; set; }
        public static List<ProductEntity> ProductList { get; set; }

        public static UserEntity UserItem { get; set; }



        #region 
        public static ProductUnitEntity ProductUnit { get; set; }
        public static ProductCodeUKTZEDEntity ProductCodeUKTZEDEntity { get; set; }
        public static UserEntity UserEntity { get; set; }
        public static UIWorkingShiftModel WorkingShift { get; set; }
        #endregion


        //private static bool _isInit = false;
        //private static IEntityGet<UserEntiti>? _users;
        //private static IEntityAccess<OperationsRecorderUserEntiti> _deviceTable;


        //public static string? NameCompany { get; set; } = string.Empty;
        //public static ProductEntiti? Product { get; set; }
        //public static List<ProductEntiti>? ProductList { get; set; }


        //public static UserEntiti UserItem { get; set; }
        public static ObservableCollection<TabItem> Tabs = new ObservableCollection<TabItem>();

        //private static UserEntiti _user;
        //public static UserEntiti User
        //{
        //    get { return _user; }
        //}
        //private static List<OperationsRecorderEntiti> _devicesSettlementOperations;
        //public static List<OperationsRecorderEntiti> Devices
        //{
        //    get { return _devicesSettlementOperations; }
        //}
        public static OperationsRecorderEntity? FocusDevices;

        //private static void Init()
        //{
        //    _users = new UserTableAccess();
        //    _deviceTable = new OperationsRecorderUserTableAccess();
        //    _user = new UserEntiti();
        //    _devicesSettlementOperations = new List<OperationsRecorderEntiti>();
        //    _isInit = true;
        //}

        public static async Task<bool> CheckSession()
        {
            try
            {
                if (User == null)
                {
                    var autoLogin = (bool)AppSettingsManager.GetParameterFiles("AutoLogin");
                    string token = AppSettingsManager.GetParameterFiles("TokenUser").ToString();

                    if (autoLogin)
                    {
                        if (token != null && token != string.Empty)
                        { 
                            await WriteSession(token); 

                            if (User != null)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        private static async Task WriteSession(string token)
        {
            User = await MainWebServerController.MainDataBaseConntroller.UserController.GetUser(token);
            Token = token;
        }

        public static void RemoveSession()
        {
            User  = null;
        }
    }
}
