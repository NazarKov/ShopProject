using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.GiftCertificatesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.GiftCertificatesPage
{
    internal class UpdateGiftCertificateModel
    {
        public UpdateGiftCertificateModel() { }
        public async Task<bool> SaveItemDataBase(GiftCertificate item)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.GiftCertificatesController.UpdateGiftCertificate(Session.User.Token, item.ToUpdateGiftCertificateDto());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
