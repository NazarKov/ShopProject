using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.Product.Interface
{
    internal interface IProductServiсe
    {
        public Task<bool> Add(ShopProject.Model.Domain.Product.Product product);
        public Task<bool> Update(ShopProject.Model.Domain.Product.Product product);
        public Task<bool> UpdateRange(List<ShopProject.Model.Domain.Product.Product> items);
        public Task<IEnumerable<ShopProject.Model.Domain.Product.Product>> UpdateProductParatemer(IEnumerable<ShopProject.Model.Domain.Product.Product> items, string parameter, object value);
        public Task<Paginator<ShopProject.Model.Domain.Product.Product>> GetPageColumn(int page, int countColumn, TypeStatusProduct status);
        public Task<Paginator<ShopProject.Model.Domain.Product.Product>> SearchByName(string item, int page, int countColumn, TypeStatusProduct statusProduct);
        public Task<Paginator<ShopProject.Model.Domain.Product.Product>> SearchByBarCode(string item, int page, int countColumn, TypeStatusProduct statusProduct);
        public Task<ProductsInfo> GetProductStatistics();

        public Task<bool> SetTypeInArhive(ShopProject.Model.Domain.Product.Product item);
        public Task<bool> SetTypeOutOfStock(ShopProject.Model.Domain.Product.Product item);

        public bool ValidationSearchitem(string nameSearch);
        public string RemoveSeparatorBarCode(string item);
        public List<ProductModel> ContertIListToList(IList list);

        public void SetProductOnSession(ShopProject.Model.Domain.Product.Product item);
        public ShopProject.Model.Domain.Product.Product GetProductOnSession();
        public void SetProductsOnSession(List<ShopProject.Model.Domain.Product.Product> items);
        public IEnumerable<ShopProject.Model.Domain.Product.Product> GetProductsOnSession();
        public Task<ShopProject.Model.Domain.Product.Product> SearchByBarCode(string item, TypeStatusProduct statusProduct);
    }
}
