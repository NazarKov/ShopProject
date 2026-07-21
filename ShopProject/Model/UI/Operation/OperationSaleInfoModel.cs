using ShopProject.Core.Mvvm;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Infrastructure.Mediator;
using System;
using System.Collections.ObjectModel; 

namespace ShopProject.Model.UI.Operation
{
    public class OperationSaleInfoModel : Model<OperationSaleInfoModel>
    {
        public Guid IDChannel;
        public decimal TotalSum;
        private decimal? _sumaOrder;
        public decimal? SumaOrder
        {
            get { return _sumaOrder; }
            set { _sumaOrder = value; OnPropertyChanged(nameof(SumaOrder)); }
        }
        private decimal? _sumaUser;
        public decimal? SumaUser
        {
            get { return _sumaUser; }
            set { _sumaUser = value; OnPropertyChanged(nameof(SumaUser)); }
        }
        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                MediatorService.ExecuteEvent(NavigationButton.CountingSumaOrder.ToString() + "" + IDChannel); OnPropertyChanged(nameof(DiscountPrecent));
                OnPropertyChanged(nameof(Discount));
            }
        }
        private decimal _discountPrecent;
        public decimal DiscountPrecent
        {
            get { return _discountPrecent; }
            set
            {
                _discountPrecent = value;
                MediatorService.ExecuteEvent(NavigationButton.CountingSumaOrder.ToString() + "" + IDChannel);
                OnPropertyChanged(nameof(DiscountPrecent));
            }
        }
        public TypePayment TypePayment;
        private ObservableCollection<ProductForSaleModel> _products;
        public ObservableCollection<ProductForSaleModel> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        private bool _draingCheck;
        public bool DrawingCheck
        {
            get { return _draingCheck; }
            set { _draingCheck = value; OnPropertyChanged(nameof(DrawingCheck)); }
        }
        private bool _isFiscalCheck;
        public bool IsFiscalCheck
        {
            get { return _isFiscalCheck; }
            set { _isFiscalCheck = value; OnPropertyChanged(nameof(IsFiscalCheck)); }
        }

        public OperationSaleInfoModel()
        {
            TotalSum = 0;
            SumaOrder = 0;
            SumaUser = 0;
            Discount = 0;
            DiscountPrecent = 0;
            IDChannel = new Guid();
            _products = new ObservableCollection<ProductForSaleModel>();
            _draingCheck = true;
            _isFiscalCheck = true;
        }

    }
}
