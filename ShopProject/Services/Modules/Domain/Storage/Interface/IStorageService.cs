using ShopProject.Model.Domain.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Domain.Storage.Interface
{
    public interface IStorageService
    {
        public StorageSetting GetSetting();
    }
}
