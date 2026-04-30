using ShopProject.Model.Domain.GiftCertificate;
using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Domain.UserRole;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Services.Modules.Resourse;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock.Properties;

namespace ShopProject.Services.Modules.Session
{
    internal class SessionService : ISessionService
    { 
        private readonly ISettingService _settingService;

        #region SessionResourse
        [Required]
        public User User { get; set; } = new User();
        [Required]
        public IEnumerable<ProductCodeUKTZED>? ProductCodesUKTZED { get; set; }
        [Required] 
        public IEnumerable<ProductUnit>? ProductUnits { get; set; } 
        [Required]
        public IEnumerable<UserRole>? Roles { get; set; }
        [Required]
        public WorkingShiftStatus WorkingShiftStatus { get; set; } = new WorkingShiftStatus();

        public ObservableCollection<TabItem> Tabs { get; set; } = new ObservableCollection<TabItem>();
        #endregion


        #region UpdateItemResourse
        public Product? UpdateProduct { get; set; }
        public IEnumerable<Product>? UpdateProductRange { get; set; } 
        public ProductUnit? UpdateProductUnit { get; set; } 
        public ProductCodeUKTZED? UpdateProductCodeUKTZED { get; set; }
        #endregion

        #region resourse
        public static User? UserItem { get; set; }
        public static ProductCodeUKTZED? ProductCodeUKTZEDEntity { get; set; }  
        public static GiftCertificate? GiftCertificate { get; set; } 

        #endregion

        public SessionService(ISettingService settingService)
        {
            _settingService = settingService; 
            
            var setting = _settingService.GetSetting<SessionSetting>();
            if(setting != null && setting.User!=null)
            {
                User = setting.User;
            }
        }

        public bool CheckingSession()
        {
            var setting = _settingService.GetSetting<SessionSetting>();
            if(User.Token == string.Empty)
            {
                if (setting.User == null)
                {
                    return false;
                }

                if (setting.User.Token == string.Empty)
                {
                    return false;
                }

                if(setting.User.AutomaticLogin == false)
                {
                    return false;
                }
                 
                User = setting.User;
                return true;
            }
            else
            {
                return true;
            }
        } 
        public bool RemoveSession()
        {
            User = new User(); 
             _settingService.SetSetting<SessionSetting>(new SessionSetting()); 
            return true;
        }
        public bool CheckingWorkingShiftStatus()
        {
            var setting = _settingService.GetSetting<WorkingShiftStatus>();

            if (setting != null && setting != new WorkingShiftStatus()) 
            {
                WorkingShiftStatus = setting;
            }

            if (WorkingShiftStatus!=null&& WorkingShiftStatus.OperationRecorder != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
      
    }
}
