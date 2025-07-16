using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject.Helpers.Template.Paginator;
using System.Windows;

namespace ShopProject.Model.StoragePage
{
    internal class UnitsOfMeasureModel
    {

        public UnitsOfMeasureModel() { }

        public async Task<PaginatorData<ProductUnitEntity>> GetUnitsPageColumn(int page, int countColumn, TypeStatusUnit statusUnit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnitsPageColumn(Session.Token, page, countColumn, statusUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductUnitEntity>();
            }
        }

        public async Task<PaginatorData<ProductUnitEntity>> SearchByName(string item, int page, int countColumn, TypeStatusUnit statusUnit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnitsByNamePageColumn(Session.Token, item, page, countColumn, statusUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductUnitEntity>();
            }
        }
        public async Task<ProductUnitEntity> SearchByBarCode(string item, TypeStatusUnit statusUnit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnitByCode(Session.Token, item, statusUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new ProductUnitEntity();
            }
        }

        public async Task<bool> Delete(ProductUnitEntity item)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.DeleteUnit(Session.Token, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> ChangeStatus(ProductUnitEntity item , TypeStatusUnit status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.UpdateParameterUnit(Session.Token,nameof(item.Status),status , item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
