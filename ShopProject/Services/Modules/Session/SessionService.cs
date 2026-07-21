using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.ProductCodeUKTZED;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.TaxObject;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Domain.UserRole;
using ShopProject.Model.Domain.WorkingShift; 
using ShopProject.Services.Modules.Session.Interface; 
using ShopProject.Services.Modules.Setting.Interface; 
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations; 
using System.Windows.Controls; 

namespace ShopProject.Services.Modules.Session
{
    internal class SessionService : ISessionService
    { 
        private readonly ISettingService _settingService;

        #region SessionResourse
        [Required]
        public User User { get; set; } = new User();
        [Required]
        public IEnumerable<ProductCodeUKTZED> ProductCodesUKTZED { get; set; }
        [Required] 
        public IEnumerable<ProductUnit> ProductUnits { get; set; } 
        [Required]
        public IEnumerable<UserRole> Roles { get; set; }
        [Required]
        public WorkingShiftStatus WorkingShiftStatus { get; set; } = new WorkingShiftStatus();

        public ObservableCollection<TabItem> Tabs { get; set; } = new ObservableCollection<TabItem>();
        #endregion


        #region UpdateItemResourse
        public Product? UpdateProduct { get; set; }
        public IEnumerable<Product>? UpdateProductRange { get; set; } 
        public ProductUnit? UpdateProductUnit { get; set; } 
        public ProductCodeUKTZED? UpdateProductCodeUKTZED { get; set; }
        public User UpdateUser { get; set; }
        public TaxObject BindingTaxObject { get; set; }
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
        public bool CheckAndLoadWorkingShiftStatus()
        { 
            var setting = _settingService.GetSetting<WorkingShiftStatus>();
            if (WorkingShiftStatus.WorkingShift != null && WorkingShiftStatus.TaxObject != null && WorkingShiftStatus.OperationRecorder != null)
            { 
                return true;
            } 
            else
            {
                WorkingShiftStatus = setting;
                return true;
            } 
        }
        public bool CheckIsOpenShift()
        {
            if (CheckAndLoadWorkingShiftStatus())
            {
                if(WorkingShiftStatus.Status == ShopProject.Model.Enum.TypeStatusShift.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
      
    }
}
