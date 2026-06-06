using ShopProject.Model.Domain.Paginator;  
using ShopProject.Model.Enum;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.ProductUnit.Interface;
using ShopProject.Services.Modules.Mapping.ProductUnit;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks; 
using ProductUnitModel = ShopProject.Model.Domain.ProductUnit.ProductUnit;

namespace ShopProject.Services.Modules.Domain.ProductUnit
{
    internal class ProductUnitServiсe : IProductUnitServiсe
    { 
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;

        public ProductUnitServiсe(IMainWebServerService mainWebServerService, ISessionService sessionService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService; 
        }


        public async Task<OperationResult<ProductUnitModel>> Add(ProductUnitModel item)
        {

            var result = new OperationResult<ProductUnitModel>();
            result.Data = item;
            result = Validation(result);
            if (result.IsError)
            {
                return result;
            }


            var response = await _webServerService.DataBase.ProductUnitController.Add(result.Data.ToCreateProductUnitDto());
            if (response.Data != null)
            {
                result.Data = response.Data.ToProductUnit(); 
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateUnitsFromSession();
            return result;
        }

        private OperationResult<ProductUnitModel> Validation(OperationResult<ProductUnitModel> item)
        {
            if (item.Data == null)
            {
                item.ErrorMessage = "Заповніть всі поля";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }
            if (item.Data.Number == 0)
            {
                item.ErrorMessage = "Ведіть номер";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.NameUnit == string.Empty)
            {
                item.ErrorMessage = "Ведіть назву коду";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }
            if (item.Data.ShortNameUnit == string.Empty)
            {
                item.ErrorMessage = "Ведіть скорочену назву коду";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }
            item.Status = ResultStatus.Success;
            return item;
        }

        public async Task<OperationResult<ProductUnitModel>> Update(ProductUnitModel item)
        {
            var result = new OperationResult<ProductUnitModel>();
            result.Data = item;
            result = Validation(result);
            if (result.IsError)
            {
                return result;
            }
            var response = await _webServerService.DataBase.ProductUnitController.Update(result.Data.ToUpdateProductUnitDto());
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateUnitsFromSession();
            return result;
        }

        public async Task<OperationResult<bool>> UpdateParameter(string parameter, object value, ProductUnitModel item)
        {
            var result = new OperationResult<bool>();

            var response = await _webServerService.DataBase.ProductUnitController.UpdateParameter(parameter, value, item.ToUpdateProductUnitDto());
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateUnitsFromSession();
            return result;
        }

        public async Task<OperationResult<bool>> Delete(ProductUnitModel item)
        {
            var result = new OperationResult<bool>();

            var response = await _webServerService.DataBase.ProductUnitController.Delete(item.ID);

            result.Data = response.Data;
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateUnitsFromSession();
            return result;
        }



        public async Task<OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>> GetPageColumn(int page, int countColumn, TypeStatusUnit status)
        {
            var result = new OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>();

            var response = await _webServerService.DataBase.ProductUnitController.GetPageColumn(new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductUnitModel, TypeStatusUnit>()
                    {
                        Data = paginator.Data.ToProductUnit(),
                        DataType = (TypeStatusUnit)paginator.DataType,
                        Page = page,
                        Pages = paginator.Pages,
                    };


                }
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>> SearchByName(string item, int page, int countColumn, TypeStatusUnit status)
        {
            var result = new OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>();

            var response = await _webServerService.DataBase.ProductUnitController.GetByNamePageColumn(item,new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductUnitModel, TypeStatusUnit>()
                    {
                        Data = paginator.Data.ToProductUnit(),
                        DataType = (TypeStatusUnit)paginator.DataType,
                        Page = page,
                        Pages = paginator.Pages,
                    };


                }
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }
        public async Task<OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>> SearchByBarCode(string item, int page, int countColumn, TypeStatusUnit status)
        {
            var result = new OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>();

            var response = await _webServerService.DataBase.ProductUnitController.GetUnitByCode(item, new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductUnitModel, TypeStatusUnit>()
                    {
                        Data = paginator.Data.ToProductUnit(),
                        DataType = (TypeStatusUnit)paginator.DataType,
                        Page = page,
                        Pages = paginator.Pages,
                    };


                }
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        } 
        public async Task<OperationResult<IEnumerable<ProductUnitModel>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<ProductUnitModel>>();

            var response = await _webServerService.DataBase.ProductUnitController.GetAll();
            if (response.Data != null)
            {
                result.Data = response.Data.ToProductUnit();
                result.Status = ResultStatus.Success;
            }

            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;
            return result;
        }

        private async Task UpdateUnitsFromSession()
        {
            var result = await GetAll();
            if (result.IsSuccess)
            {
                _sessionService.ProductUnits = result.Data;
            }
            else
            {
                throw new Exception(result.ErrorMessage);
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
                var result = await GetAll();
                if (result.IsSuccess)
                {
                    codes = result.Data;
                } 
                else
                {
                    throw new Exception(result.ErrorMessage);
                }
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
