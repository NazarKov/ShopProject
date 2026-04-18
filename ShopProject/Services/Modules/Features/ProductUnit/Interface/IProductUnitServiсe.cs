using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.ProductUnit.Interface
{
    public interface IProductUnitServiсe
    {
        public Task<bool> Add(ShopProject.Model.Domain.ProductUnit.ProductUnit unit);
        public Task<bool> Update(ShopProject.Model.Domain.ProductUnit.ProductUnit unit);
        public Task<bool> Delete(ShopProject.Model.Domain.ProductUnit.ProductUnit item);

        public Task<Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>> GetUnitsPageColumn(int page, int countColumn, TypeStatusUnit statusUnit);
        public Task<Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>> SearchByName(string item, int page, int countColumn, TypeStatusUnit statusUnit);

        public Task<Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>> SearchByBarCode(string item, int page, int countColumn, TypeStatusUnit statusUnit);
        public Task<bool> ChangeStatus(ShopProject.Model.Domain.ProductUnit.ProductUnit item, TypeStatusUnit status);
        public Task<IEnumerable<ShopProject.Model.Domain.ProductUnit.ProductUnit>> GetFromSession();
        public void SetUnitOnSession(ShopProject.Model.Domain.ProductUnit.ProductUnit item);
        public ShopProject.Model.Domain.ProductUnit.ProductUnit GetUnitFromSession();
    }
}
