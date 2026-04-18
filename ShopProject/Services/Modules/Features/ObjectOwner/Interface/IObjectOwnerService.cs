using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ObjectOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.ObjectOwner.Interface
{
    internal interface IObjectOwnerService
    {
        public Task<Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>> GetObjectOwnerPageColumn(int page, int countColumn, TypeStatusObjectOwner status);
        public Task<Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>> SearchByName(string item, int page, int countColumn, TypeStatusObjectOwner status);
        public Task<bool> GetServerObjectOwner(string pathFile, string passwordKey);
        public bool SaveDataBaseItem(List<ObjectOwnerDialogWindowModel> objectOwnerHelpers);
        public List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> GetListObjectOwner();
        public Task<bool> DeleteItem(ShopProject.Model.Domain.ObjectOwner.ObjectOwner item);
        public Task<List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>> GetObjectOwners();
        public void SetObjectOwnerOnWorkingShiftStatusInSession(ShopProject.Model.Domain.ObjectOwner.ObjectOwner objectOwner);
        public ShopProject.Model.Domain.ObjectOwner.ObjectOwner GetObjectOwnerOnWorkingShiftStatusFromSession();
    }
}
