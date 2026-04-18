using ShopProject.Model.Domain.Paginator; 
using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface
{
    public interface IProductCodeUKTZEDServiсe
    {
        public Task<bool> Add(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED codeUKTZED);
        public Task<bool> Update(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED unit);
        public Task<bool> Delete(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED item);

        public Task<IEnumerable<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> GetFromSession();
        public Task<bool> ChangeStatus(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED item, TypeStatusCodeUKTZED status);
        public Task<Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> GetCodeUKTZEDPageColumn(int page, int countColumn, TypeStatusCodeUKTZED status);
        public Task<Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> SearchByBarCode(string item, int page, int countColumn, TypeStatusCodeUKTZED status);
        public Task<Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> SearchByName(string item, int page, int countColumn, TypeStatusCodeUKTZED status); 

        public void SetOnSession(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED item);
        public ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED GetProductCodeUKTZEDFromSession();
    }
}
