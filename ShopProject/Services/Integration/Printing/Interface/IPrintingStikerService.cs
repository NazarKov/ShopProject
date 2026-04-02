using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ShopProject.Services.Integration.Printing.Interface
{
    internal interface IPrintingStikerService
    {
        public void SetShowTextInImage(bool isShowNameCompany, bool isShowProductBarCode, bool isShowProductName, bool isShowProductDescription);
        public BitmapImage CreateBarCode(string company, string name, string description, string code);
        public BitmapImage Clear();
        public void Print();
        public string GetNameCompany();
    }
}
