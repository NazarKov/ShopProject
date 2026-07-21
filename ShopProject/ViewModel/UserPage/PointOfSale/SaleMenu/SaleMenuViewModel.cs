using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.Operation;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface;
using ShopProject.Services.Modules.Domain.Product.Interface;
using ShopProject.Services.Modules.Mapping.Operation;
using ShopProject.Services.Modules.Mapping.Product;
using ShopProject.Services.Modules.Model.WorkingShift.Interface;
using ShopProject.View.UserPage.PointOfSale.SaleMenu.PaymentMethod;
using System; 
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage.PointOfSale.SaleMenu
{
    internal class SaleMenuViewModel : ViewModel<SaleMenuViewModel>, IViewModelLoadResourse
    {  
        private ICommand _searchBarCodeCommand;
        private ICommand _clearFieldDataGrid;

        private ICommand _setCashPaymentMethod;
        private ICommand _setCardPaymentMethod;


        private ICommand _printingCheckCommand;
      
        private ICommand _sendReturnCheckCommand;
        private Guid _idChannel;
        private User _user;
        private StorageSetting _setting; 

        private ISaleMenuService _saleMenuService;
        private IProductServiсe _productServiсe;
        private IWorkingShiftService _workingShiftService;
         
        public SaleMenuViewModel(ISaleMenuService saleMenuService , IProductServiсe productServiсe, IWorkingShiftService workingShiftService)
        {
            _saleMenuService = saleMenuService;
            _productServiсe = productServiсe;
            _searchBarCodeCommand = CreateCommandAsync(DebounceSearch);
            _clearFieldDataGrid = CreateCommand(ClearField);
            _setCashPaymentMethod = CreateCommand(() => { PaymentMenthod = new CashMethodView(); OperationSaleInfo.TypePayment = TypePayment.Cash; VisibilitiCheckMenu = Visibility.Visible; });
            _setCardPaymentMethod = CreateCommand(() => { PaymentMenthod = new CashMethodView(); OperationSaleInfo.TypePayment = TypePayment.Card; VisibilitiCheckMenu = Visibility.Visible; });

            _printingCheckCommand = CreateCommandAsync(PrintingCheck); 
            _sendReturnCheckCommand = CreateCommandAsync(ReturnCheck);

            _operationSaleInfo = new OperationSaleInfoModel(); 
            _barCodeSearch = string.Empty;  
            _user = new User(); 
            _setting = new StorageSetting(); 
            _isEnableSendCheckButton = false;
            PaymentMenthod = new UserControl();
            _visibilitiCheckMenu = Visibility.Collapsed;
            _workingShiftService = workingShiftService;
        }
        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
            SafeExecute(ClearField);
            return Task.CompletedTask;
        } 
        private UserControl _paymentMenthod;
        public UserControl PaymentMenthod
        {
            get { return _paymentMenthod; }
            set {  _paymentMenthod = value; OnPropertyChanged(nameof(PaymentMenthod));}
        }  
        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged(nameof(BarCodeSearch)); }
        }

        private OperationSaleInfoModel _operationSaleInfo;
        public OperationSaleInfoModel OperationSaleInfo
        {
            get { return _operationSaleInfo; }
            set { _operationSaleInfo = value; OnPropertyChanged(nameof(OperationSaleInfo)); } 
        }   
        private bool _isEnableSendCheckButton;
        public bool IsEnableSendCheckButton
        {
            get { return _isEnableSendCheckButton; }
            set { _isEnableSendCheckButton = value; OnPropertyChanged(nameof(IsEnableSendCheckButton)); }
        }
        private Visibility _visibilitiCheckMenu;
        public Visibility VisibilitiCheckMenu
        {
            get { return _visibilitiCheckMenu; }
            set { _visibilitiCheckMenu = value; OnPropertyChanged(nameof(VisibilitiCheckMenu)); }
        }
        public ICommand ClearFieldDataGid => _clearFieldDataGrid;
        private void ClearField()
        {
            VisibilitiCheckMenu = Visibility.Collapsed;
            PaymentMenthod = new UserControl();
            OperationSaleInfo = new OperationSaleInfoModel(); 
            _idChannel = Guid.NewGuid();
            MediatorService.AddEvent(NavigationButton.CountingSumaOrder.ToString() + "" + _idChannel, CountingSumaOrder);
            MediatorService.AddEvent<object>(NavigationButton.RemoveProduct.ToString() + "" + _idChannel, RemoveItem);
            EnableButton();
        } 
        private void SetFieldPage()
        { 
            //DrawingCheck = _saleMenuService.IsDrawinfChek;

            //_user = _saleMenuService.GetUserFromSession(); 
        } 

        public ICommand SearchBarCodeCommand => _searchBarCodeCommand;
         
        private CancellationTokenSource? _searchCts;
        private async Task DebounceSearch()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(500, _searchCts.Token); // 500 мс очікування  
                await SearchBarCodeProduct();
            }
            catch (TaskCanceledException)  { }
        } 

        private async Task SearchBarCodeProduct()
        {
            ObservableCollection<ProductForSaleModel> temp;
            var result = await _productServiсe.SearchByBarCode(BarCodeSearch);
            if (result.IsSuccess)
            {
                var item = result.Data.ToProductModel();
                if (item != null)
                {

                    item.Count = 1;
                    temp = new ObservableCollection<ProductForSaleModel>();
                    temp = OperationSaleInfo.Products;

                    if (temp.FirstOrDefault(pr => pr.Product.Code == item.Code) != null)
                    {
                        temp.FirstOrDefault(pr => pr.Product.Code == item.Code).Count += 1;
                    }
                    else
                    {
                        temp.Add(new ProductForSaleModel(item.ToProduct()) { Cannnel = _idChannel });
                    }

                    CountingSumaOrder();

                    OperationSaleInfo.Products = new ObservableCollection<ProductForSaleModel>();
                    OperationSaleInfo.Products = temp;
                }
            }
            else if (result.IsError && result.ErrorType == Services.Modules.Common.Enum.ErrorType.Validation)
            {
                return;
            }
            else if (result.IsError && result.ErrorType == Services.Modules.Common.Enum.ErrorType.DeleteBarCode)
            {
                if (OperationSaleInfo.Products.Count() != 0)
                {
                    if (OperationSaleInfo.Products.ElementAt(OperationSaleInfo.Products.Count - 1).Count == 1)
                    {
                        temp = new ObservableCollection<ProductForSaleModel>();
                        temp = OperationSaleInfo.Products;

                        temp.Remove(temp.ElementAt(temp.Count - 1));
                        OperationSaleInfo.Products = new ObservableCollection<ProductForSaleModel>();
                        OperationSaleInfo.Products = temp;
                        CountingSumaOrder();
                    }
                    else
                    {
                        temp = new ObservableCollection<ProductForSaleModel>();
                        temp = OperationSaleInfo.Products;


                        temp.ElementAt(OperationSaleInfo.Products.Count - 1).Count -= 1;
                        OperationSaleInfo.Products = new ObservableCollection<ProductForSaleModel>();
                        OperationSaleInfo.Products = temp;
                        CountingSumaOrder();
                    }
                }
            }
            BarCodeSearch = string.Empty;
        }

        private void CountingSumaOrder()
        {
            OperationSaleInfo.SumaOrder = 0;
            foreach (ProductForSaleModel orderProduct in OperationSaleInfo.Products)
            {
                OperationSaleInfo.SumaOrder += (orderProduct.Product.Price * orderProduct.Count);
            }
            OperationSaleInfo.TotalSum = OperationSaleInfo.SumaOrder.Value;
            if (OperationSaleInfo.DiscountPrecent != 0)
            {
                OperationSaleInfo.SumaOrder = OperationSaleInfo.SumaOrder - (OperationSaleInfo.SumaOrder * (OperationSaleInfo.DiscountPrecent / 100));
            }
            if (OperationSaleInfo.Discount != 0)
            {
                OperationSaleInfo.SumaOrder = OperationSaleInfo.SumaOrder - OperationSaleInfo.Discount;
            }
            if (OperationSaleInfo.SumaOrder < 0)
            {
                OperationSaleInfo.SumaOrder = OperationSaleInfo.TotalSum;
                OperationSaleInfo.Discount = 0;
                OperationSaleInfo.DiscountPrecent = 0;
            }
            EnableButton();
        }

        private void EnableButton()
        {
            if (OperationSaleInfo.Products.Count <= 0)
            {
                IsEnableSendCheckButton = false;
            }
            else
            {
                IsEnableSendCheckButton = true;
            }
        }

        private void RemoveItem(object item)
        {
            var product = item as ProductForSaleModel;
            if (product != null)
            {
                OperationSaleInfo.Products.Remove(product);
                CountingSumaOrder();
            }
        }
        public ICommand AddNumberToTextBoxCommand { get => CreateCommandParameter<object>(AddNumberToTextBoxField); }
        private void AddNumberToTextBoxField(object parameter)
        {
            if (parameter != null)
            {
                OperationSaleInfo.SumaUser += Convert.ToInt32(parameter);
            }
        }
        public ICommand SetCashPaymetnMethod => _setCashPaymentMethod;
        public ICommand SetCardPaymentMethod => _setCardPaymentMethod;


        public ICommand PrintingCheckCommand => _printingCheckCommand;
        private async Task PrintingCheck()
        {
            try
            {
                IsEnableSendCheckButton = false;  
                if (!(OperationSaleInfo.SumaUser >= OperationSaleInfo.SumaOrder))
                {
                    throw new Exception("Сума внеску не може бути менша ніж сума чеку");
                }
                else
                {
                    await _saleMenuService.SendCheck(OperationSaleInfo.ToOperationInfoSale());

                    MessageBox.Show("ok");
                    IsEnableSendCheckButton = true;
                    ClearField();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                IsEnableSendCheckButton = true;
            }
        }


        public ICommand SendReturnCheckCommand => _sendReturnCheckCommand;
        private async Task ReturnCheck()
        {
            //try
            //{
            //    IsEnableSendCheckButton = false;
            //   // _workingShiftService.LoadSaleMenuDataFromFile();
            //    _saleMenuService.IsDrawinfChek = DrawingCheck;

            //    if (!(SumaUser >= SumaOrder))
            //    {
            //        MessageBox.Show("Сума внеску не може бути менша ніж сума чеку");
            //    }
            //    else
            //    {
            //        _saleMenuService.AddKey(_user.SignatureKey);

            //        var rest = (SumaUser - SumaOrder);
            //        var discount = new Discount();

            //        if (DiscountPrecent != 0)
            //        {
            //            discount.TotalDiscount = _totalSum * (DiscountPrecent / 100);
            //            discount.TypeDiscount = 1;
            //            discount.CreateAt = DateTime.Now;
            //            discount.InterimAmount = _totalSum;
            //            discount.Rebate = DiscountPrecent;
            //        }
            //        else if (Discount != 0)
            //        {
            //            discount.TotalDiscount = Discount;
            //            discount.TypeDiscount = 0;
            //            discount.CreateAt = DateTime.Now;
            //            discount.InterimAmount = _totalSum;
            //            discount.Rebate = Discount;
            //        }
            //        else
            //        {
            //            discount = null;
            //        }

            //        Operation operation = new Operation()
            //        {
            //            TypeOperation = (Model.Enum.TypeOperation)TypeOperation.ReturnCheck,
            //            MAC = await _saleMenuService.GetMAC(),
            //            CreatedAt = DateTime.Now,
            //            NumberPayment = await _saleMenuService.GetLocalNumber(),
            //            GoodsTax = "0",
            //            RestPayment = rest.Value,
            //            TotalPayment = _totalSum,
            //            BuyersAmount = SumaUser.Value,
            //            TypePayment = (Model.Enum.TypePayment)(TypePayment)SelectTypePayment,
            //            Discount = discount,
            //        };

            //        if (await _saleMenuService.SendCheck(Product, operation))

            //        {
            //            MessageBox.Show("Товар повернено", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            //            Product = new ObservableCollection<ProductForSaleModel>();
            //            BarCodeSearch = string.Empty;
            //            SumaUser = new decimal();
            //            SumaUser = 0;
            //            SumaOrder = 0; 
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        } 
    }
}
