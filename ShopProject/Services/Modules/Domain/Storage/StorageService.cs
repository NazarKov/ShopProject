using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Modules.Domain.Storage.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System; 

namespace ShopProject.Services.Modules.Domain.Storage
{
    public class StorageService : IStorageService
    {
        private ISettingService _settingService;
        public StorageService(ISettingService settingService) 
        {
            _settingService = settingService;
        }

        public StorageSetting GetSetting()
        {
            return _settingService.GetSetting<StorageSetting>();
        }
    }
}
