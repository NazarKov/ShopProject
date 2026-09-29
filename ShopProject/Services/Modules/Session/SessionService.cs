using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.OperationRecorder;
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
using System.Threading.Tasks;
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
        public IEnumerable<ProductCodeUKTZED> ProductCodesUKTZED { get; set; } = new List<ProductCodeUKTZED>();
        [Required]
        public IEnumerable<ProductUnit> ProductUnits { get; set; } = new List<ProductUnit>();
        [Required]
        public IEnumerable<UserRole> Roles { get; set; } = new List<UserRole>();
        [Required]
        public WorkingShiftStatus WorkingShiftStatus { get; set; } = new WorkingShiftStatus();

        public ObservableCollection<TabItem> Tabs { get; set; } = new ObservableCollection<TabItem>();
        #endregion


        #region UpdateItemResourse
        public Product? UpdateProduct { get; set; } 
        public IEnumerable<Product>? UpdateProductRange { get; set; } 
        public ProductUnit? UpdateProductUnit { get; set; } 
        public ProductCodeUKTZED? UpdateProductCodeUKTZED { get; set; }
        public User? UpdateUser { get; set; }
        public TaxObject? BindingTaxObject { get; set; }
        public OperationRecorder? UpdateOperationRecorder { get; set; }
        #endregion

        #region TempResourse
        /// <summary>
        /// success OperationMessageWorkingShiftMenu
        /// </summary>
        public Operation Operation { get; set; } = new Operation();
        #endregion

        public SessionService(ISettingService settingService )
        {
            _settingService = settingService;  

        }

        public async Task<bool> CheckingSession()
        {
            var setting = _settingService.GetSetting<SessionSetting>();
            if (setting.User!=null && setting.User.Token != string.Empty)
            {
                this.User = setting.User;
                return true;
            }
            else
            {
                return false;
            } 
        } 
        public void SaveSession()
        {
            _settingService.SetSetting<SessionSetting>(new SessionSetting()
            {
                User = this.User
            });
        }
        public void RemoveSession()
        { 
            _settingService.SetSetting<WorkingShiftStatus>(new WorkingShiftStatus());  
        }
        public bool CheckAndLoadWorkingShiftStatus()
        { 
            var setting = _settingService.GetSetting<WorkingShiftStatus>();
            if(WorkingShiftStatus == null)
            {
                return false;
            }
            else if (setting == null)
            {
                return false;
            }
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
            if (WorkingShiftStatus.Status == ShopProject.Model.Enum.TypeStatusShift.Open)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        public bool CheckShiftSetting()
        {
            if (CheckAndLoadWorkingShiftStatus())
            {
                if (User.ID == WorkingShiftStatus.UserID)
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
