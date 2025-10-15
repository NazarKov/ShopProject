using ShopProject.Helpers; 
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage
{
    internal class UnitsOfMeasureModel
    {
        private readonly string _token; 

        public UnitsOfMeasureModel()
        {
            _token = Session.User.Token;
        }

        public async Task<PaginatorData<ProductUnit>> GetUnitsPageColumn(int page, int countColumn, TypeStatusUnit statusUnit)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnitsPageColumn(_token, page, countColumn, statusUnit);

                var paginator = new PaginatorData<ProductUnit>()
                {
                    Data = result.Data.ToProductUnit(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductUnit>();
            }
        }

        public async Task<PaginatorData<ProductUnit>> SearchByName(string item, int page, int countColumn, TypeStatusUnit statusUnit)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnitsByNamePageColumn(_token, item, page, countColumn, statusUnit);

                var paginator = new PaginatorData<ProductUnit>()
                {
                    Data = result.Data.ToProductUnit(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductUnit>();
            }
        }
        public async Task<ProductUnit> SearchByBarCode(string item, TypeStatusUnit statusUnit)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnitByCode(_token, item, statusUnit)).ToProductUnit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new ProductUnit();
            }
        }

        public async Task<bool> Delete(ProductUnit item)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.DeleteUnit(_token, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> ChangeStatus(ProductUnit item , TypeStatusUnit status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.UpdateParameterUnit(_token, nameof(item.Status),status , item.ToUpdateProductUnit());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
