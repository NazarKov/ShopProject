using ShopProject.Helpers; 
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System; 
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage
{
    internal class ProductCodeUKTZEDModel
    {
        private readonly string _token;
        public ProductCodeUKTZEDModel()
        {
            _token = Session.User.Token;
        }

        public async Task<PaginatorData<ProductCodeUKTZED>> GetCodeUKTZEDPageColumn(int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZEDPageColumn(_token, page, countColumn, status);

                var paginator = new PaginatorData<ProductCodeUKTZED>()
                {
                    Data = result.Data.ToProductCodeUKTZED(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductCodeUKTZED>();
            }
        }

        public async Task<PaginatorData<ProductCodeUKTZED>> SearchByName(string item, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZEDByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new PaginatorData<ProductCodeUKTZED>()
                {
                    Data = result.Data.ToProductCodeUKTZED(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductCodeUKTZED>();
            }
        }
        public async Task<ProductCodeUKTZED> SearchByBarCode(string item, TypeStatusCodeUKTZED status)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZEDEByCode(_token, item, status)).ToProductCodeUKTZED();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new ProductCodeUKTZED();
            }
        }

        public async Task<bool> Delete(ProductCodeUKTZED item)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.DeleteCodeUKTZED(_token, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> ChangeStatus(ProductCodeUKTZED item, TypeStatusCodeUKTZED status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.UpdateParameterCodeUKTZED(_token, nameof(item.Status), status, item.ToUpdateProductCodeUKTZED());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
