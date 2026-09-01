using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Enum;  
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Product; 
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.Product.Interface;
using ShopProject.Services.Modules.Domain.ProductCodeUKTZED.Interface; 
using ShopProject.Services.Modules.Domain.ProductUnit.Interface;
using ShopProject.Services.Modules.Mapping.Product; 
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ProductModel = ShopProject.Model.Domain.Product.Product;

namespace ShopProject.Services.Modules.Domain.Product
{
    internal class ProductServiсe :IProductServiсe
    {
        private readonly string _token;
        private readonly StorageSetting _setting;
        private IMainWebServerService _webServerService;
        private ISettingService _settingService;
        private ISessionService _sessionService;
        private IProductCodeUKTZEDServiсe _productCodeUKTZEDServiсe;
        private IProductUnitServiсe _productUnitServiсe;

        public ProductServiсe(ISettingService settingService,IMainWebServerService mainWebServerService,ISessionService sessionService , 
            IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe , IProductUnitServiсe productUnitServiсe)
        {
            _webServerService = mainWebServerService;
            _settingService = settingService; 
            _sessionService = sessionService;
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe;
            _productUnitServiсe = productUnitServiсe;

            _setting = _settingService.GetSetting<StorageSetting>();
            _token = _sessionService.User.Token;
        }

        public async Task<OperationResult<ProductModel>> Add(ProductModel product)
        {
            product.CreatedAt = DateTime.Now;

            var result = new OperationResult<ProductModel>();
            result.Data = product;

            result = Validation(result);
            if (result.IsError)
            {
                return result;
            }

            result = DeleteSpace(result);

            if (result.IsError)
            {
                return result;
            }

            var response = await _webServerService.DataBase.ProductController.Add(result.Data.ToCreateProductDto());
            if (response.Data != null)
            {
                result.Data = response.Data.ToProduct(await _productCodeUKTZEDServiсe.GetFromSession(), await _productUnitServiсe.GetFromSession());
            }

            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        private OperationResult<ProductModel> Validation(OperationResult<ProductModel> item)
        {
            if (item.Data == null)
            {
                item.ErrorMessage = "Заповніть всі поля";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (_setting == null)
            {
                item.ErrorMessage = "Ведіть в налаштуваннях довжину штрихкоду";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.NotFound;
                return item;
            }
            if (item.Data.NameProduct == string.Empty)
            {
                item.ErrorMessage = "Ведіть назву товару";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.Code == string.Empty)
            {
                item.ErrorMessage = "Ведіть штрихкод товару";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.Code.Count() != _setting.ProductBarCodeLength)
            {
                item.ErrorMessage = "Довжина штрихкоду не " + _setting.ProductBarCodeLength + " символів";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;

            }

            if (!item.Data.Code.All(char.IsDigit))
            {
                item.ErrorMessage = "Ведіть штрихкод тільки з чисел";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.Price == 0)
            {
                item.ErrorMessage = "Ведіть ціну товару";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.Count == 0)
            {
                item.ErrorMessage = "Ведіть кількість товару";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.Articule == string.Empty)
            {
                item.ErrorMessage = "Ведіть артикуль товару";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            item.Status = ResultStatus.Success;
            return item;
        }
 
        private OperationResult<ProductModel> DeleteSpace(OperationResult<ProductModel> item)
        {
            if (item.Data == null)
            {
                item.ErrorMessage = "Заповніть всі поля";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }
            item.Data.NameProduct = item.Data.NameProduct.Trim();
            item.Data.Code = item.Data.Code.Trim();
            item.Data.Articule = item.Data.Articule.Trim();

            item.Status = ResultStatus.Success;
            return item;
        }
         
        public async Task<OperationResult<ProductModel>> Update(ProductModel product)
        {
            var result = new OperationResult<ProductModel>();
            result.Data = product;

            result = Validation(result);
            if (result.IsError)
            {
                return result;
            }
            result = DeleteSpace(result);

            if (result.IsError)
            {
                return result;
            }

            var response = await _webServerService.DataBase.ProductController.Update(product.ToUpdateProductDto());
              
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<bool>> UpdateParameter(string parameter , object value, ProductModel item)
        {
            var result = new OperationResult<bool>();
            var response = await _webServerService.DataBase.ProductController.UpdateParameter(parameter, value, item.ToUpdateProductDto());

            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors; 
            return result;
        }

        public async Task<OperationResult<bool>> UpdateRange(List<ProductModel> items)
        {
            var result = new OperationResult<bool>();
              
            var response = await _webServerService.DataBase.ProductController.UpdateRange(items.ToUpdateProductDto());

            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;
            return result;
        } 
        public OperationResult<IEnumerable<ProductModel>> ChangeParameterList(string parameter, object value, IEnumerable<ProductModel> items)
        {
            var result = new OperationResult<IEnumerable<ProductModel>>();
            if (value == null)
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessage = "Вкажіть значення для зміни";
                return result;
            }

            foreach (var item in items)
            {
                switch (parameter)
                {
                    case nameof(item.Count):
                        {
                            item.Count = decimal.Parse(((double)value).ToString());
                            break;
                        }
                    case nameof(item.Price):
                        {
                            item.Price = decimal.Parse(((double)value).ToString());
                            break;
                        }
                }
            }
            result.Data = items;
            result.Status = ResultStatus.Success;
            return result;;
        }
         
        public async Task<OperationResult<Paginator<ProductModel, TypeStatusProduct>>> GetPageColumn(int page, int countColumn, TypeStatusProduct status)
        {
            var result = new OperationResult<Paginator<ProductModel, TypeStatusProduct>>();

            var response = await _webServerService.DataBase.ProductController.GetPageColumn(new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductModel, TypeStatusProduct>()
                    {
                        Data = paginator.Data.ToProduct((await _productCodeUKTZEDServiсe.GetFromSession()),(await _productUnitServiсe.GetFromSession())),
                        DataType = (TypeStatusProduct)paginator.DataType,
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

        public async Task<OperationResult<Paginator<ProductModel, TypeStatusProduct>>> SearchByName(string item, int page, int countColumn, TypeStatusProduct status)
        {
            var result = new OperationResult<Paginator<ProductModel, TypeStatusProduct>>();

            var response = await _webServerService.DataBase.ProductController.GetByNamePageColumn(item,new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductModel, TypeStatusProduct>()
                    {
                        Data = paginator.Data.ToProduct((await _productCodeUKTZEDServiсe.GetFromSession()), (await _productUnitServiсe.GetFromSession())),
                        DataType = (TypeStatusProduct)paginator.DataType,
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
        public async Task<OperationResult<Paginator<ProductModel, TypeStatusProduct>>> SearchByBarCode(string item, int page, int countColumn, TypeStatusProduct status)
        {
            var result = new OperationResult<Paginator<ProductModel, TypeStatusProduct>>();

            var response = await _webServerService.DataBase.ProductController.GetProductsByBarCode(item, new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<ProductModel, TypeStatusProduct>()
                    {
                        Data = paginator.Data.ToProduct((await _productCodeUKTZEDServiсe.GetFromSession()), (await _productUnitServiсe.GetFromSession())),
                        DataType = (TypeStatusProduct)paginator.DataType,
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

        public async Task<OperationResult<ProductModel>> SearchByBarCode(string barCode)
        {
            try
            {
                var result = new OperationResult<ProductModel>();

                result = await CheckBarCodeToDeleteCode(barCode);
                if (result.IsError)
                {
                    return result;
                }
                result = await CheckProductToSearch(barCode); 

                if (result.IsSuccess)
                {
                    var response = await _webServerService.DataBase.ProductController.GetProductByBarCode(barCode);
                    result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                    result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                    result.ErrorMessage = response.Error;
                    result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                    result.ValidationErrors = response.Errors;
                    if (result.IsSuccess)
                    {
                        result.Data = response.Data.ToProduct(_sessionService.ProductCodesUKTZED, _sessionService.ProductUnits);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return OperationResult<ProductModel>.Fail(ex.Message);
            }
        } 

        private async Task<OperationResult<ProductModel>> CheckProductToSearch(string item)
        {
            if (item.Length == 13)
            {
                var regex = "^\\d{" + _setting.ProductBarCodeLength + "}.?$";//визначення сепаратора
                MatchCollection matchCollection = Regex.Matches(item, regex);
                if (matchCollection.Count > 0)
                {
                    item = matchCollection[0].ToString().Split('═').ElementAt(0);// = сеператор сканера
                }
                if (item.Count() == _setting.ProductBarCodeLength && Regex.Matches(item, "[1-9]").Any())
                {
                    return OperationResult<ProductModel>.Success(new ProductModel());
                } 
            } 
            else if(item.Length < _setting.ProductBarCodeLength)
            {
                return OperationResult<ProductModel>.Fail("Штрихкод менше 13 символів",ErrorType.Validation);
            }

            return OperationResult<ProductModel>.Fail("Штрихкод недорівнює 13 символів");
        }

        private async Task<OperationResult<ProductModel>> CheckBarCodeToDeleteCode(string barCode)
        {
            if(barCode == _settingService.GetSetting<OperationRecorderSetting>().DeleteBarCode)
            {
                return OperationResult<ProductModel>.Fail("Штрих код видалення",ErrorType.DeleteBarCode);
            }
            else
            {
                return OperationResult<ProductModel>.Success(new ProductModel());
            }
        }


        public string RemoveSeparatorBarCode(string item)
        {
            item = item.Split('═', '=').ElementAt(0);
            return item;
        }


        public async Task<ProductsInfo> GetProductStatistics()
        {
            try
            {
                return (await _webServerService.DataBase.ProductController.GetProductInfo(_token)).ToProductsInfo();
            }
            catch (Exception ex)
            { 
                return new ProductsInfo();
            }
        } 

        public List<ProductModel> ContertIListToList(IList list)
        { 
            var products = new List<ProductModel>();
            foreach (ShopProject.Model.UI.Product.ProductModel item in list)
            {
                products.Add(item.ToProduct());
            }
            return products;
        } 
     
         

        public void SetProductOnSession(ShopProject.Model.Domain.Product.Product item)
        {
            _sessionService.UpdateProduct = item;
        }
        public ShopProject.Model.Domain.Product.Product GetProductOnSession()
        {
            var item = _sessionService.UpdateProduct; 
            return item;
        }
        public void SetProductsOnSession(List<ShopProject.Model.Domain.Product.Product> items)
        {
            _sessionService.UpdateProductRange = items;
        }
        public IEnumerable<ShopProject.Model.Domain.Product.Product> GetProductsOnSession()
        {
            var items = _sessionService.UpdateProductRange;
            if (items == null)
            {
                throw new Exception("Невдалося завантажити товар");
            }
            return items;
        } 
        public async Task<IEnumerable<ShopProject.Model.Domain.Product.Product>> GetItems()
        {
            try
            {
                var items = (await _webServerService.DataBase.ProductController.GetProducts(_token)).ToProduct(await _productCodeUKTZEDServiсe.GetFromSession(),await _productUnitServiсe.GetFromSession());
                return items;
            }
            catch (Exception ex)
            {
                return null;
            }
        } 
    }

}
