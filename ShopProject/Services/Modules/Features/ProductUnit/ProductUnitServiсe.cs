using ShopProject.Helpers;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping; 
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.ProductUnit
{
    internal class ProductUnitServiсe : IProductUnitServiсe
    {
        private readonly string _token;
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;

        public ProductUnitServiсe(IMainWebServerService mainWebServerService, ISessionService sessionService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService;
            _token = _sessionService.User.Token;
        }


        public async Task<bool> Add(ShopProject.Model.Domain.ProductUnit.ProductUnit unit)
        {

            if (unit.Number == 0)
            {
                throw new ExceptionStringEmpty("Не вели номер одиниці");
            }
            if (unit.NameUnit == string.Empty)
            {
                throw new ExceptionStringEmpty("Не вели назву одиниці");
            }
            if (unit.ShortNameUnit == string.Empty)
            {
                throw new ExceptionStringEmpty("Не вели скорочену назву одиниці");
            }

            return await _webServerService.DataBase.ProductUnitController.AddProductUnit(_token, unit.ToProductUnit());
        }

        public async Task<bool> Update(ShopProject.Model.Domain.ProductUnit.ProductUnit unit)
        {
            try
            {
                return await _webServerService.DataBase.ProductUnitController.UpdateUnit(_token, unit.ToUpdateProductUnit());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>> GetUnitsPageColumn(int page, int countColumn, TypeStatusUnit statusUnit)
        {
            try
            {
                var result = await _webServerService.DataBase.ProductUnitController.GetUnitsPageColumn(_token, page, countColumn, statusUnit);

                var paginator = new Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>()
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
                return new Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>();
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>> SearchByName(string item, int page, int countColumn, TypeStatusUnit statusUnit)
        {
            var result = await _webServerService.DataBase.ProductUnitController.GetUnitsByNamePageColumn(_token, item, page, countColumn, statusUnit);


            var paginator = new Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>()
            {
                Data = result.Data.ToProductUnit(),
                DataType = result.DataType,
                Page = result.Page,
                Pages = result.Pages,
            };
            return paginator;
        }
        public async Task<Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>> SearchByBarCode(string item, int page, int countColumn, TypeStatusUnit statusUnit)
        {
            var result = (await _webServerService.DataBase.ProductUnitController.GetUnitByCode(_token, item, page, countColumn, statusUnit));
            return new Paginator<ShopProject.Model.Domain.ProductUnit.ProductUnit>()
            {
                Data = result.Data.ToProductUnit(),
                DataType = result.DataType,
                Page = result.Page,
                Pages = result.Pages,
            };
        }

        public async Task<bool> Delete(ShopProject.Model.Domain.ProductUnit.ProductUnit item)
        {
            try
            {
                return await _webServerService.DataBase.ProductUnitController.DeleteUnit(_token, item);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ChangeStatus(ShopProject.Model.Domain.ProductUnit.ProductUnit item, TypeStatusUnit status)
        {
            try
            {
                return await _webServerService.DataBase.ProductUnitController.UpdateParameterUnit(_token, nameof(item.Status), status, item.ToUpdateProductUnit());
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<ShopProject.Model.Domain.ProductUnit.ProductUnit>> GetFromSession()
        {
            var codes = _sessionService.ProductUnits;
            if (codes != null)
            {
                return codes.ToList();
            }
            else
            {
                codes = (await _webServerService.DataBase.ProductUnitController.GetUnits(_token)).ToProductUnit();
            }
            if (codes != null)
            {
                return codes;
            }

            throw new Exception("Невдалося завантажити одиниці виміру");
        }

        public void SetUnitOnSession(ShopProject.Model.Domain.ProductUnit.ProductUnit item)
        {
            _sessionService.UpdateProductUnit = item;
        }

        public ShopProject.Model.Domain.ProductUnit.ProductUnit GetUnitFromSession()
        {
            var result = _sessionService.UpdateProductUnit;

            if(result != null)
            {
                return result;
            } 
            throw new Exception("Невдалося завантажити одиницю");
        }

    }
}
