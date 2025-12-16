using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.GiftCertificatesPage;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZXing.Aztec.Internal;

namespace ShopProject.Model.GiftCertificatesPage
{
    public class GiftCertificatesModel
    {
        private readonly string _token;
        public GiftCertificatesModel() 
        {
            _token = Session.User.Token;
        }

        public async Task<PaginatorData<GiftCertificate>> GetGiftCertificatePageColumn(int page, int countColumn, TypeStatusGiftCertificate status)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.GiftCertificatesController.GetGiftCertificatePageColumn(_token, page, countColumn, status);

                var paginator = new PaginatorData<GiftCertificate>()
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
                return new PaginatorData<GiftCertificate>();
            }
        }

        public async Task<PaginatorData<GiftCertificate>> SearchByName(string item, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.GiftCertificatesController.GetGiftCertificateByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new PaginatorData<GiftCertificate>()
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
                return new PaginatorData<GiftCertificate>();
            }
        }
        public async Task<GiftCertificate> SearchByBarCode(string item, TypeStatusGiftCertificate status)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.GiftCertificatesController.GetGiftCertificateByBarCode(_token, item, status)).ToGiftCertificate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new GiftCertificate();
            }
        }

        public async Task<bool> SetItemInArhive(GiftCertificate item)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.GiftCertificatesController.UpdateParameterGiftCertificate(_token, nameof(item.Status), TypeStatusGiftCertificate.Archived, item.ToUpdateGiftCertificateDto());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public void ContertIListToList(IList list, List<GiftCertificate> giftCertificates)
        {
            foreach (GiftCertificate item in list)
            {
                giftCertificates.Add(item);
            }
        }
    }
}
