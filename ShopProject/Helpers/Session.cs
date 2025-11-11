using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;  
using ShopProject.UIModel;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.StoragePage;
using ShopProject.UIModel.UserPage; 
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic; 
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls; 

namespace ShopProject.Helpers
{
    public class Session
    {
        public static User? User { get; set; }  
         
        public static List<ProductEntity> ProductList { get; set; }


        #region resourse
        public static User? UserItem { get; set; }       
        public static ProductCodeUKTZED? ProductCodeUKTZEDEntity { get; set; }
        public static ProductUnit? ProductUnit { get; set; }
        public static Product? Product { get; set; } 
        public static OperationRecorder? FocusDevices;
        public static WorkingShift? WorkingShift { get; set; }
        public static IEnumerable<UserRole>? Roles { get; set; }
        public static IEnumerable<ProductCodeUKTZED>? ProductCodesUKTZED { get; set; }
        public static IEnumerable<ProductUnit>? ProductUnits { get; set; }
        #endregion


        #region  
        public static UserEntity UserEntity { get; set; }
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

        //private static void Init()
        //{
        //    _users = new UserTableAccess();
        //    _deviceTable = new OperationsRecorderUserTableAccess();
        //    _user = new UserEntiti();
        //    _devicesSettlementOperations = new List<OperationsRecorderEntiti>();
        //    _isInit = true;
        //}

        public static bool CheckSession()
        {
            try
            { 
                if (User == null)
                { 
                    var user = User.Deserialize(AppSettingsManager.GetParameterFiles("User").ToString());
                    User = user;  
                    if (user != null)
                    {
                        InitResourse();
                        user.AutomaticLogin = true;
                        if (user.Token == null && user.Token == string.Empty)
                        {
                            return false;
                        }
                        if (!user.AutomaticLogin)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    return true;
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
        public static void RemoveSession()
        {
            User  = null;
            AppSettingsManager.SetParameterFile("User", string.Empty);
        } 
        private static void InitResourse()
        {
            if (Roles == null)
            {
                Resources.InitWebServerResourses();
            } 
        }
    }
}
