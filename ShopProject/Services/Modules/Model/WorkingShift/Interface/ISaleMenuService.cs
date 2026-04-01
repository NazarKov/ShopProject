using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.UI.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Model.WorkingShift.Interface
{
    internal interface ISaleMenuService
    {
        public void AddKey(SignatureKey key);
        public Task<bool> SendCheck(ObservableCollection<ProductForSaleModel> products, Operation operation);
        public void PrintCheck(List<Product> products, Operation operation, string id);
        public Task<MediaAccessControl> GetMAC();
        public Task<string> GetLocalNumber();
        public bool IsDrawinfChek { get; set; }
    }
}
