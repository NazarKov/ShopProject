using ShopProject.Helpers;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Product;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping; 
using ShopProject.Services.Modules.ModelService.Product.Interface;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface;
using ShopProject.Services.Modules.ModelService.User.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Services.Modules.ModelService.Product
{
    internal class ProductServiсe : IProductServiсe
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

        public async Task<Paginator<ShopProject.Model.Domain.Product.Product>> GetPageColumn(int page, int countColumn, TypeStatusProduct status)
        {
            try
            {
                var result = await _webServerService.DataBase.ProductController.GetProductsPageColumn(_token, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.Product.Product>()
                {
                    Data = result.Data.ToProduct(await _productCodeUKTZEDServiсe.GetFromSession(),await _productUnitServiсe.GetFromSession()),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.Product.Product>();
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.Product.Product>> SearchByName(string item, int page, int countColumn, TypeStatusProduct statusProduct)
        {
            try
            {
                var result = await _webServerService.DataBase.ProductController.GetProductByNamePageColumn(_token, item, page, countColumn, statusProduct);

                var paginator = new Paginator<ShopProject.Model.Domain.Product.Product>()
                {
                    Data = result.Data.ToProduct(await _productCodeUKTZEDServiсe.GetFromSession(),await _productUnitServiсe.GetFromSession()),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            { 
                return new Paginator<ShopProject.Model.Domain.Product.Product>();
            }
        }
        public async Task<Paginator<ShopProject.Model.Domain.Product.Product>> SearchByBarCode(string item, int page, int countColumn, TypeStatusProduct statusProduct)
        {
            try
            {
                var result = await _webServerService.DataBase.ProductController.GetProductsByBarCode(_token, item, page, countColumn, statusProduct);
                if (result == null)
                {
                    throw new Exception();
                }

                return new Paginator<ShopProject.Model.Domain.Product.Product>(result.Page, result.Pages, result.Data.ToProduct(await _productCodeUKTZEDServiсe.GetFromSession(), await _productUnitServiсe.GetFromSession()));
            }
            catch (Exception ex)
            {
                return new Paginator<ShopProject.Model.Domain.Product.Product>();
            }
        }
        public async Task<ShopProject.Model.Domain.Product.Product> SearchByBarCode(string item,TypeStatusProduct statusProduct)
        {
            try
            {
                var result = new ProductDto();

                if (item.Length > _setting.ProductBarCodeLength - 1 && item.Length <= _setting.ProductBarCodeLength + 1)
                {
                    var regex = "^\\d{" + _setting.ProductBarCodeLength + "}.?$";//визначення сепаратора
                    MatchCollection matchCollection = Regex.Matches(item, regex);
                    if (matchCollection.Count > 0)
                    {
                        item = matchCollection[0].ToString().Split('═').ElementAt(0);// = сеператор сканера
                    }
                    if (item.Count() == _setting.ProductBarCodeLength && Regex.Matches(item, "[1-9]").Any())
                    {
                        result = await _webServerService.DataBase.ProductController.GetProductByBarCode(_token, item, statusProduct); 
                    }
                }
                else if (item.Length > _setting.ProductBarCodeLength + 1)
                {
                    item = string.Empty;
                }

                if (result == null&&result == new ProductDto())
                {
                    throw new Exception();
                }

                return result.ToProduct(_sessionService.ProductCodesUKTZED, _sessionService.ProductUnits);
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public bool ValidationSearchitem(string item)
        { 
            if (Regex.Matches(item, "[0-9]").Any())
            {
                return true;
            }
            else
            {
                return false;
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
                //MessageBox.Show(ex.Message);
                return new ProductsInfo();
            }
        }

        public async Task<bool> SetTypeInArhive(ShopProject.Model.Domain.Product.Product item)
        {
            try
            {
                return await _webServerService.DataBase.ProductController.UpdateParameterProduct(_token, nameof(item.Status), TypeStatusProduct.Archived, item.ToUpdateProductDto());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public async Task<bool> SetTypeOutOfStock(ShopProject.Model.Domain.Product.Product item)
        {
            try
            {
                return await _webServerService.DataBase.ProductController.UpdateParameterProduct(_token, nameof(item.Status), TypeStatusProduct.OutStock, item.ToUpdateProductDto());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public List<ProductModel> ContertIListToList(IList list)
        {
            var products = new List<ProductModel>();
            foreach (ProductModel item in list)
            {
                products.Add(item);
            }
            return products;
        }

        public async Task<bool> UpdateRange(List<ShopProject.Model.Domain.Product.Product> items)
        { 
            return await _webServerService.DataBase.ProductController.UpdateProductRange(_token, items.ToUpdateProductDto());
        }

        public async Task<IEnumerable<ShopProject.Model.Domain.Product.Product>> UpdateProductParatemer(IEnumerable<ShopProject.Model.Domain.Product.Product> items ,string parameter,object value)
        {
            if(value == null)
            {
                throw new Exception("Value is null");
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
            return items;
        }


        public async Task<bool> Update(ShopProject.Model.Domain.Product.Product product)
        {
            Validation(product);
            return await _webServerService.DataBase.ProductController.UpdateProduct(_token, product.ToUpdateProductDto()); 
        }

        public async Task<bool> Add(ShopProject.Model.Domain.Product.Product product)
        {
            product.CreatedAt = DateTime.Now;
            Validation(product);
            return await _webServerService.DataBase.ProductController.AddProduct(_token, product.ToCreateProductDto());
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

        public async Task<ShopProject.Model.Domain.Product.Product> GetItem(string itemSearch)
        {
            try
            {
                var item = (await _webServerService.DataBase.ProductController.GetProductByBarCode(_token, itemSearch)).ToProduct(await _productCodeUKTZEDServiсe.GetFromSession(),await _productUnitServiсe.GetFromSession());
                return item;
            }
            catch (Exception ex)
            {
                return new ShopProject.Model.Domain.Product.Product();
            }
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

        public async Task<ShopProject.Model.Domain.Product.Product>? Search(string barCode)
        {
            try
            {
                var item = await _webServerService.DataBase.ProductController.GetProductByBarCode(_token, barCode);
                if (item == null)
                {
                    return null;
                }
                return item.ToProduct(await _productCodeUKTZEDServiсe.GetFromSession(),await _productUnitServiсe.GetFromSession());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void Validation(ShopProject.Model.Domain.Product.Product product)
        {

            if (_setting == null)
            {
                throw new Exception("Ведіть в налаштуваннях довжину штрихкоду ");
            }
            if (product.NameProduct == string.Empty)
            {
                throw new ExceptionStringEmpty("Ведіть назву товару");
            }

            if (product.Code == string.Empty)
            {
                throw new ExceptionStringEmpty("Ведіть штрихкод товару");
            }

            if (product.Code.Count() != _setting.ProductBarCodeLength)
            {
                throw new ExceptionStringEmpty("Довжина штрихкоду не " + _setting.ProductBarCodeLength + " символів");

            }

            if (!product.Code.All(char.IsDigit))
            {
                throw new ExceptionStringEmpty("Ведіть штрихкод тільки з чисел");
            }

            if (product.Price == 0)
            {
                throw new ExceptionStringEmpty("Ведіть ціну товару");
            }

            if (product.Count == 0)
            {
                throw new ExceptionStringEmpty("Ведіть кількість товару");
            }

            if (product.Articule == string.Empty)
            {
                throw new ExceptionStringEmpty("Ведіть артикуль товару");
            }
        }
    
    }

}
