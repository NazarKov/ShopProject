using NPOI.SS.Formula.UDF;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Views.AdminPage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace ShopProject.Helpers
{
    public class Session
    {
        private static bool _isInit = false;
        private static IEntityGet<UserEntiti>? _users;
        private static IEntityAccess<OperationsRecorderUserEntiti> _deviceTable;
        

        public static string? NameCompany { get; set; } = string.Empty;
        public static ProductEntiti? Product { get; set; }
        public static List<ProductEntiti>? ProductList { get; set; }


        public static UserEntiti UserItem { get; set; }
        public static ObservableCollection<TabItem> Tabs = new ObservableCollection<TabItem>();

        private static UserEntiti _user;
        public static UserEntiti User
        {
            get { return _user; }
        }
        private static List<OperationsRecorderEntiti> _devicesSettlementOperations;
        public static List<OperationsRecorderEntiti> Devices
        {
            get { return _devicesSettlementOperations; }
        }
        public static OperationsRecorderEntiti? FocusDevices;

        private static void Init()
        {
            _users = new UserTableAccess();
            _deviceTable = new OperationsRecorderUserTableAccess();
            _user = new UserEntiti();
            _devicesSettlementOperations = new List<OperationsRecorderEntiti>();
            _isInit = true;
        }

        public static bool CheckSession()
        {
            var login =  AppSettingsManager.GetParameterFiles("SessionID");
            
            if(login != null && login != string.Empty)
            {
                GetInformationDataBase((string)login);
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void GetInformationDataBase(string login )
        {
            _devicesSettlementOperations = new List<OperationsRecorderEntiti>();
            if(!_isInit)
            {
                Init();
            }
            var users = _users.GetAll();
            _user = _users.GetAll().Where(item => item.Login.Equals(login)).FirstOrDefault();


            var device = _deviceTable.GetAll().Where(item => item.Users.ID == _user.ID).ToList();

            if(device != null)
            {
                foreach(var item in device)
                {
                    _devicesSettlementOperations.Add(item.OpertionsRecorders);
                }
            }

            var devise = AppSettingsManager.GetParameterFiles("FocusDevise").ToString();
            if (devise != "")
            {
                FocusDevices = _devicesSettlementOperations.Where(item => item.ID.ToString() == devise).FirstOrDefault();
            }
        }

        public static void Add(UserEntiti user)
        {
            GetInformationDataBase(user.Login);
            AppSettingsManager.SetParameterFile("SessionID", user.Login);
        }
        public static void Remove()
        {
            _user = null;
            _devicesSettlementOperations = null;
            AppSettingsManager.SetParameterFile("SessionID", null);
        }
        

    }
}
