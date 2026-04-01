using ShopProject.Helpers;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping; 
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Services.Modules.ModelService.ProductCodeUKTZED
{
    internal class ProductCodeUKTZEDServiсe  : IProductCodeUKTZEDServiсe
    {
        private readonly string _token;
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;

        public ProductCodeUKTZEDServiсe(IMainWebServerService mainWebServerService,ISessionService sessionService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService;
            _token = _sessionService.User.Token;
        }

        public async Task<bool> Add(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED codeUKTZED)
        {
            if(codeUKTZED.Code == string.Empty)
            {
                throw new ExceptionStringEmpty("Не вказаний Код");
            }
            if (codeUKTZED.NameCode == string.Empty)
            {
                throw new ExceptionStringEmpty("Не вказана Назва");
            }

            return await _webServerService.DataBase.ProductCodeUKTZEDController.AddProductCodeUKTZED(_token, codeUKTZED.ToProductCodeUKTZED());
        }

        public async Task<bool> Update(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED unit)
        {
            try
            {
                return await _webServerService.DataBase.ProductCodeUKTZEDController.UpdateCodeUKTZED(_token, unit.ToUpdateProductCodeUKTZED());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> GetCodeUKTZEDPageColumn(int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                var result = await _webServerService.DataBase.ProductCodeUKTZEDController.GetCodeUKTZEDPageColumn(_token, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>()
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
                return new Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>();
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> SearchByName(string item, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                var result = await _webServerService.DataBase.ProductCodeUKTZEDController.GetCodeUKTZEDByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>()
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
                return new Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>();
            }
        }
        public async Task<Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> SearchByBarCode(string item, int page, int countColumn, TypeStatusCodeUKTZED status)
        {

            var result = (await _webServerService.DataBase.ProductCodeUKTZEDController.GetCodesUKTZEDEByCode(_token, item, page, countColumn, status));

            if(result != null && result.Data!=null)
            {
                return new Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>()
                {
                    Data = result.Data.ToProductCodeUKTZED(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
            }

            return new Paginator<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>();
        }

        public async Task<bool> Delete(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED item)
        {
            try
            {
                return await _webServerService.DataBase.ProductCodeUKTZEDController.DeleteCodeUKTZED(_token, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> ChangeStatus(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED item, TypeStatusCodeUKTZED status)
        {
            try
            {
                return await _webServerService.DataBase.ProductCodeUKTZEDController.UpdateParameterCodeUKTZED(_token, nameof(item.Status), status, item.ToUpdateProductCodeUKTZED());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        } 
        public async Task<IEnumerable<ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED>> GetFromSession()
        {
            var codes = _sessionService.ProductCodesUKTZED;
            if (codes != null)
            {
                return codes.ToList();
            }
            else
            {
                codes = (await _webServerService.DataBase.ProductCodeUKTZEDController.GetCodeUKTZED(_token)).ToProductCodeUKTZED();
            }

            if (codes != null)
            {
                return codes;
            }


            throw new Exception("Невдалося завантажити продуктові коди"); 
        }

        public void SetOnSession(ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED item)
        {
            _sessionService.UpdateProductCodeUKTZED = item;
        }

        public ShopProject.Model.Domain.PorductCodeUKTZED.ProductCodeUKTZED GetProductCodeUKTZEDFromSession()
        {
            var result = _sessionService.UpdateProductCodeUKTZED;
            if(result  != null)
            {
                return result;
            }
            throw new Exception("Невдалося завантажити");
        }
    }
}
