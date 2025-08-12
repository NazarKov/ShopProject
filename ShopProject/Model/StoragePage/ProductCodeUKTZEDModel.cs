using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using System; 
using System.Threading.Tasks;
using ShopProject.Helpers.Template.Paginator;
using System.Windows;

namespace ShopProject.Model.StoragePage
{
    internal class ProductCodeUKTZEDModel
    {
        public ProductCodeUKTZEDModel() { }

        public async Task<PaginatorData<ProductCodeUKTZEDEntity>> GetCodeUKTZEDPageColumn(int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZEDPageColumn(Session.Token, page, countColumn, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductCodeUKTZEDEntity>();
            }
        }

        public async Task<PaginatorData<ProductCodeUKTZEDEntity>> SearchByName(string item, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZEDByNamePageColumn(Session.Token, item, page, countColumn, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductCodeUKTZEDEntity>();
            }
        }
        public async Task<ProductCodeUKTZEDEntity> SearchByBarCode(string item, TypeStatusCodeUKTZED status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZEDEByCode(Session.Token, item, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new ProductCodeUKTZEDEntity();
            }
        }

        public async Task<bool> Delete(ProductCodeUKTZEDEntity item)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.DeleteCodeUKTZED(Session.Token, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> ChangeStatus(ProductCodeUKTZEDEntity item, TypeStatusCodeUKTZED status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.UpdateParameterCodeUKTZED(Session.Token, nameof(item.Status), status, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
