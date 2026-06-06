using ShopProject.Model.Domain.Paginator; 
using ShopProject.Model.Enum; 
using ShopProject.Model.UI.ProductUnit;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.Mapping.ProductCodeUKTZED;
using ShopProject.Services.Modules.Mapping.ProductUnit;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using ProductCodeUKTZEDModel = ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED;

namespace ShopProject.Services.Modules.Domain.ProductCodeUKTZED
{
    internal class ProductCodeUKTZEDServiсe  : IProductCodeUKTZEDServiсe
    { 
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;

        public ProductCodeUKTZEDServiсe(IMainWebServerService mainWebServerService,ISessionService sessionService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService; 
        }

        public async Task<OperationResult<ProductCodeUKTZEDModel>> Add(ProductCodeUKTZEDModel item)
        { 
            var result = new OperationResult<ProductCodeUKTZEDModel>();
            result.Data = item;
            result = Validation(result);
            if (result.IsError)
            {
                return result;
            }


            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.Add(result.Data.ToCreateProductCodeUKTZEDDto());
            if(response.Data!=null)
            {
                result.Data = response.Data.ToProductCodeUKTZED();
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateCodeUKTZEDFromSession();
            return result;
        }
        private OperationResult<ProductCodeUKTZEDModel> Validation(OperationResult<ProductCodeUKTZEDModel> item)
        {
            if (item.Data == null) 
            {
                item.ErrorMessage = "Заповніть всі поля";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }
            if (item.Data.Code == string.Empty)
            {
                item.ErrorMessage = "Ведіть товарний Код";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.NameCode == string.Empty)
            {
                item.ErrorMessage = "Ведіть назву товарного коду";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            } 
            item.Status = ResultStatus.Success;
            return item;
        }

        public async Task<OperationResult<ProductCodeUKTZEDModel>> Update(ProductCodeUKTZEDModel item)
        {
            var result = new OperationResult<ProductCodeUKTZEDModel>();
            result.Data = item;
            result = Validation(result);
            if (result.IsError)
            {
                return result;
            }
            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.Update(result.Data.ToUpdateProductCodeUKTZEDDto());
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateCodeUKTZEDFromSession();
            return result;
        }

        public async Task<OperationResult<bool>> UpdateParameter(string parameter, object value, ProductCodeUKTZEDModel item)
        {
            var result = new OperationResult<bool>(); 

            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.UpdateParameter(parameter,value,item.ToUpdateProductCodeUKTZEDDto());
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateCodeUKTZEDFromSession();
            return result;
        }

        public async Task<OperationResult<bool>> Delete(ProductCodeUKTZEDModel item)
        {
            var result = new OperationResult<bool>();

            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.Delete(item.ID);

            result.Data = response.Data;
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            await UpdateCodeUKTZEDFromSession();
            return result;
        }

        public async Task<OperationResult<Paginator<ProductCodeUKTZEDModel,TypeStatusCodeUKTZED>>> GetPageColumn(int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            var result = new OperationResult<Paginator<ProductCodeUKTZEDModel, TypeStatusCodeUKTZED>>();

            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.GetPageColumn(new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductCodeUKTZEDModel, TypeStatusCodeUKTZED>()
                    {
                        Data = paginator.Data.ToProductCodeUKTZED(),
                        DataType = (TypeStatusCodeUKTZED)paginator.DataType,
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

        public async Task<OperationResult<Paginator<ProductCodeUKTZEDModel,TypeStatusCodeUKTZED>>> SearchByName(string item, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            var result = new OperationResult<Paginator<ProductCodeUKTZEDModel, TypeStatusCodeUKTZED>>();

            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.GetByNamePageColumn(item,new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductCodeUKTZEDModel, TypeStatusCodeUKTZED>()
                    {
                        Data = paginator.Data.ToProductCodeUKTZED(),
                        DataType = (TypeStatusCodeUKTZED)paginator.DataType,
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
        public async Task<OperationResult<Paginator<ProductCodeUKTZEDModel,TypeStatusCodeUKTZED>>> SearchByBarCode(string item, int page, int countColumn, TypeStatusCodeUKTZED status)
        {

            var result = new OperationResult<Paginator<ProductCodeUKTZEDModel, TypeStatusCodeUKTZED>>();

            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.GetByCode(item, new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductCodeUKTZEDModel, TypeStatusCodeUKTZED>()
                    {
                        Data = paginator.Data.ToProductCodeUKTZED(),
                        DataType = (TypeStatusCodeUKTZED)paginator.DataType,
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
        public async Task<OperationResult<IEnumerable<ProductCodeUKTZEDModel>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<ProductCodeUKTZEDModel>>();

            var response = await _webServerService.DataBase.ProductCodeUKTZEDController.GetAll();
            if (response.Data != null)
            {
                result.Data = response.Data.ToProductCodeUKTZED();
                result.Status = ResultStatus.Success;
            }

            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;
            return result;
        }
        private async Task UpdateCodeUKTZEDFromSession()
        {
            var result = await GetAll();
            if (result.IsSuccess)
            {
                _sessionService.ProductCodesUKTZED = result.Data;
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED>> GetFromSession()
        {
            var codes = _sessionService.ProductCodesUKTZED;
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


            throw new Exception("Невдалося завантажити продуктові коди"); 
        }

        public void SetOnSession(ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED item)
        {
            _sessionService.UpdateProductCodeUKTZED = item;
        }

        public ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED GetProductCodeUKTZEDFromSession()
        {
            var result = _sessionService.UpdateProductCodeUKTZED;
            if (result != null)
            {
                return result;
            }
            throw new Exception("Невдалося завантажити");
        }
    }
}
