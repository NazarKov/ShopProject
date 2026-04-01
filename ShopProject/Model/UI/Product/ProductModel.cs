using ShopProject.Core.Mvvm;
using ShopProject.Model.Domain.Discount;
using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.Discount;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Model.UI.ProductUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.Model.UI.Product
{
    public class ProductModel : Model<ProductModel>
    {
        public Guid ID { get; set; }
        private string _code = string.Empty;
        public string Code
        {
            get { return _code; }
            set { _code = value; BarcodeLength = _code.Length; OnPropertyChanged(nameof(BarcodeLength)); }
        }
        public int BarcodeLength { get; set; }
        public string NameProduct { get; set; } = string.Empty;
        public string Articule { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public decimal Count { get; set; } = decimal.Zero; 
        public DiscountModel? Discount { get; set; }
        public TypeStatusProduct Status { get; set; } 
        public ProductUnitModel? Unit { get; set; }
        public int UnitId { get { return Unit.ID; } set { Unit.ID = value; } }
        public ProductCodeUKTZEDModel? CodeUKTZED { get; set; }
        public int CodeUKTZEDId { get { return CodeUKTZED.ID; } set { CodeUKTZED.ID = value; } }
        public string StatusString 
        { 
            get { return ProductStatusModel.GetProductStatus().ElementAt(System.Enum.GetValues<TypeStatusProduct>().ToList().IndexOf(Status)); }
            set { Status = System.Enum.GetValues<TypeStatusProduct>().ToList().ElementAt(ProductStatusModel.GetProductStatus().IndexOf(value)); } 
        }

        private Visibility _shadowVisibility = Visibility.Collapsed;
        public Visibility ShadowVisibility { get { return _shadowVisibility; } set { _shadowVisibility = value; OnPropertyChanged(nameof(ShadowVisibility)); } }

        private ICommand? _lostFocusCommand;
        public ICommand? LostFocusCommand { get { return _lostFocusCommand; } set{ _lostFocusCommand = value;OnPropertyChanged(nameof(LostFocusCommand)); } }
          
    }
}
