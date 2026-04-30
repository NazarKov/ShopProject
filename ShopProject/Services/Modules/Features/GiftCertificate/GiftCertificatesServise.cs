using ShopProject.Helpers;
using ShopProject.Model.Domain.GiftCertificate;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Integration.Network.WebServerApi.Mapping;
using ShopProject.Services.Modules.ModelService.GiftCertificate.Interface;
using ShopProject.Services.Modules.NetworkUrlManager;
using ShopProject.Services.Modules.Setting.Interface;
using ShopProjectDataBase.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZXing.Aztec.Internal;

namespace ShopProject.Services.Modules.ModelService.GiftCertificate
{
    internal class GiftCertificatesServise : IGiftCertificatesServise
    {
        private readonly string _token;

        private IMainWebServerService _webServerService;

        public GiftCertificatesServise(IMainWebServerService mainWebServerService)
        { 
            _webServerService = mainWebServerService; 
        }

        //public async Task<bool> Add(Model.Domain.GiftCertificate.GiftCertificate item)
        //{
        //    try
        //    {
        //        return await MainWebServerController.MainDataBaseConntroller.GiftCertificatesController.AddGiftCertificates(Session.User.Token, item.ToCreateGiftCertificate());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }
        //}

        //public async Task<bool> Update(Model.Domain.GiftCertificate.GiftCertificate item)
        //{
        //    try
        //    {
        //        return await MainWebServerController.MainDataBaseConntroller.GiftCertificatesController.UpdateGiftCertificate(Session.User.Token, item.ToUpdateGiftCertificateDto());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }
        //}

        public async Task<Paginator<ShopProject.Model.Domain.GiftCertificate.GiftCertificate>> GetGiftCertificatePageColumn(int page, int countColumn, TypeStatusGiftCertificate status)
        {
            try
            {
                var result = await _webServerService.DataBase.GiftCertificatesController.GetGiftCertificatePageColumn(_token, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.GiftCertificate.GiftCertificate>()
                {
                    Data = result.Data.ToGiftCertificate(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.GiftCertificate.GiftCertificate>();
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.GiftCertificate.GiftCertificate>> SearchByName(string item, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            try
            {
                var result = await _webServerService.DataBase.GiftCertificatesController.GetGiftCertificateByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.GiftCertificate.GiftCertificate>()
                {
                    Data = result.Data.ToGiftCertificate(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.GiftCertificate.GiftCertificate>();
            }
        }
        public async Task<ShopProject.Model.Domain.GiftCertificate.GiftCertificate> SearchByBarCode(string item, TypeStatusGiftCertificate status)
        {
            try
            {
                return (await _webServerService.DataBase.GiftCertificatesController.GetGiftCertificateByBarCode(_token, item, status)).ToGiftCertificate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new ShopProject.Model.Domain.GiftCertificate.GiftCertificate();
            }
        }

        public async Task<bool> SetItemInArhive(ShopProject.Model.Domain.GiftCertificate.GiftCertificate item)
        {
            try
            {
                return await _webServerService.DataBase.GiftCertificatesController.UpdateParameterGiftCertificate(_token, nameof(item.Status), TypeStatusGiftCertificate.Archived, item.ToUpdateGiftCertificateDto());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public void ContertIListToList(IList list, List<ShopProject.Model.Domain.GiftCertificate.GiftCertificate> giftCertificates)
        {
            foreach (ShopProject.Model.Domain.GiftCertificate.GiftCertificate item in list)
            {
                giftCertificates.Add(item);
            }
        }
    }
}
