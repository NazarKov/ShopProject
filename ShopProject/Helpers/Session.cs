using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;  
using ShopProject.UIModel;
using ShopProject.UIModel.GiftCertificatesPage;
using ShopProject.UIModel.ObjectOwnerPage;
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
         


        #region resourse
        public static User? UserItem { get; set; }       
        public static ProductCodeUKTZED? ProductCodeUKTZEDEntity { get; set; }
        public static ProductUnit? ProductUnit { get; set; }
        public static Product? Product { get; set; }  
        public static List<Product>? UpdateProductRange { get; set; } 
        public static IEnumerable<UserRole>? Roles { get; set; }
        public static IEnumerable<ProductCodeUKTZED>? ProductCodesUKTZED { get; set; }
        public static IEnumerable<ProductUnit>? ProductUnits { get; set; }    
        public static WorkingShiftStatus? WorkingShiftStatus { get; set; } 
        public static GiftCertificate? GiftCertificate { get; set; }
        #endregion


        #region  
        public static UserEntity UserEntity { get; set; }
        #endregion
         
        public static ObservableCollection<TabItem> Tabs = new ObservableCollection<TabItem>();

 

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
            WorkingShiftStatus = new WorkingShiftStatus();
            var item = WorkingShiftStatus.Deserialize(AppSettingsManager.GetParameterFiles("WorkingShiftStatus").ToString());
            if (item != null) 
            {
                WorkingShiftStatus = item;
            }
        }
        public static void LoadSaleMenuDataFromFile()
        { 
            var json = AppSettingsManager.GetParameterFiles("WorkingShiftStatus").ToString(); 
            if (json == null)
            {
                throw new Exception();
            }
            var workingshiftStatus = WorkingShiftStatus.Deserialize(json);
            if (workingshiftStatus == null)
            {
                workingshiftStatus = new WorkingShiftStatus();
            }
            else
            {
                if(WorkingShiftStatus != null)
                {
                    if(WorkingShiftStatus.OperationRecorder == null)
                    {
                        WorkingShiftStatus.OperationRecorder = workingshiftStatus.OperationRecorder;
                    }
                    if (WorkingShiftStatus.WorkingShift == null)
                    {
                        WorkingShiftStatus.WorkingShift = workingshiftStatus.WorkingShift;
                    }

                    WorkingShiftStatus.StatusShift = workingshiftStatus.StatusShift;
                    WorkingShiftStatus.StatusOnline = workingshiftStatus.StatusOnline;
                    WorkingShiftStatus.MediaAccessControl = workingshiftStatus.MediaAccessControl;
                    WorkingShiftStatus.ObjectOwner = workingshiftStatus.ObjectOwner;
                } 
            }
        }
    }
}
