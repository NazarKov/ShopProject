using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Discount;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Model.WorkingShift.Interface;
using ShopProject.Services.Modules.ModelService.Product.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage.SaleMenu
{
    internal class SaleProductMenuViewModel : ViewModel<SaleProductMenuViewModel>, IViewModelLoadResourse
    { 

        private ICommand _searchBarCodeGoodsCommand;
        private ICommand _printingCheckCommand;
        private ICommand _clearFieldDataGrid; 
        private ICommand _cleareSumUserCommand;
        private ICommand _sendReturnCheckCommand;
        private Guid _idChannel;
        private User _user;
        private StorageSetting _setting;
        private OperationRecorderSetting _settingOperationRecorder;

        private ISaleMenuService _saleMenuService;
        private IProductServiсe _productServiсe;
        private IWorkingShiftService _workingShiftService;
         
        public SaleProductMenuViewModel(ISaleMenuService saleMenuService , IProductServiсe productServiсe, IWorkingShiftService workingShiftService)
        {
            _saleMenuService = saleMenuService;
            _productServiсe = productServiсe;
            _searchBarCodeGoodsCommand = CreateCommandAsync(DebounceSearch);
            _printingCheckCommand = CreateCommandAsync(PrintingCheck); 
            _clearFieldDataGrid = CreateCommand(ClearField);
            _cleareSumUserCommand = CreateCommand(ClearSumUser);
            _sendReturnCheckCommand = CreateCommandAsync(ReturnCheck);

            _product = new ObservableCollection<ProductForSaleModel>();
            _barCodeSearch = string.Empty;
            _typeOplatu = new List<string>();
            _draingCheck = true;
            IsFiscalCheck = true;
            _totalSum = decimal.Zero;
            _user = new User();
            _selectTypePayment = 0;
            _setting = new StorageSetting();
            _settingOperationRecorder = new OperationRecorderSetting();
            _isEnableSendCheckButton = false; 

            _workingShiftService = workingShiftService;
        }
        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
            SafeExecute(ClearField);
            return Task.CompletedTask;
        }

        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged(nameof(BarCodeSearch)); }
        }

        private ObservableCollection<ProductForSaleModel> _product;
        public ObservableCollection<ProductForSaleModel> Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        private decimal _totalSum;
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
        private List<string> _typeOplatu;
        public List<string> TypeOplatu
        {
            get { return _typeOplatu; }
            set { _typeOplatu = value; OnPropertyChanged(nameof(TypeOplatu)); }
        }
        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                MediatorService.ExecuteEvent(NavigationButton.CountingSumaOrder.ToString() + "" + _idChannel); OnPropertyChanged(nameof(DiscountPrecent));
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
                MediatorService.ExecuteEvent(NavigationButton.CountingSumaOrder.ToString() + "" + _idChannel);
                OnPropertyChanged(nameof(DiscountPrecent));
            }
        }

        private int _selectTypePayment;
        public int SelectTypePayment
        {
            get { return _selectTypePayment; }
            set { _selectTypePayment = value; OnPropertyChanged(nameof(SelectTypePayment)); }
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
        public int Tag { get; set; } = 0;

        private bool _isEnableSendCheckButton;
        public bool IsEnableSendCheckButton
        {
            get { return _isEnableSendCheckButton; }
            set { _isEnableSendCheckButton = value; OnPropertyChanged(nameof(IsEnableSendCheckButton)); }
        } 
        public ICommand ClearFieldDataGid => _clearFieldDataGrid;
        private void ClearField()
        {
            Product = new ObservableCollection<ProductForSaleModel>();
            SumaUser = 0;
            SumaOrder = 0;
            SelectTypePayment = 0;

            _idChannel = Guid.NewGuid();
            MediatorService.AddEvent(NavigationButton.CountingSumaOrder.ToString() + "" + _idChannel, CountingSumaOrder);
            MediatorService.AddEvent<object>(NavigationButton.RemoveProduct.ToString() + "" + _idChannel, RemoveItem);
            EnableButton();
        }

        private void SetFieldPage()
        {
            _typeOplatu.Add("готівкою");
            _typeOplatu.Add("безготівкові форми оплати");
            _typeOplatu.Add("Сертифікат");

            SelectTypePayment = 0;
            DrawingCheck = _saleMenuService.IsDrawinfChek;

            _user = _saleMenuService.GetUserFromSession(); 
        }


        public ICommand SearchBarCodeGoodsCommand => _searchBarCodeGoodsCommand;


        private CancellationTokenSource? _searchCts;
        private async Task DebounceSearch()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(500, _searchCts.Token); // 400 мс очікування  
                await SearchBarCodeProduct();
            }
            catch (TaskCanceledException)
            {
                // ігноруємо — користувач продовжив вводити
            }
        }



        private async Task SearchBarCodeProduct()
        {
            ObservableCollection<ProductForSaleModel> temp;
            if (BarCodeSearch != _settingOperationRecorder.DeleteBarCode)
            {  
                var item = await _productServiсe.SearchByBarCode(BarCodeSearch, TypeStatusProduct.Unknown);
                if (item != null)
                {

                    item.Count = 1;
                    temp = new ObservableCollection<ProductForSaleModel>();
                    temp = Product;

                    if (temp.FirstOrDefault(pr => pr.Product.Code == item.Code) != null)
                    {
                        temp.FirstOrDefault(pr => pr.Product.Code == item.Code).Count += 1;
                    }
                    else
                    {
                        temp.Add(new ProductForSaleModel(item) { Cannnel = _idChannel });
                    }

                    CountingSumaOrder();

                    Product = new ObservableCollection<ProductForSaleModel>();
                    Product = temp;
                }
                BarCodeSearch = string.Empty;
            }
            else
            {
                if (Product.Count() != 0)
                {
                    if (Product.ElementAt(Product.Count - 1).Count == 1)
                    {
                        temp = new ObservableCollection<ProductForSaleModel>();
                        temp = Product;

                        temp.Remove(temp.ElementAt(temp.Count - 1));
                        Product = new ObservableCollection<ProductForSaleModel>();
                        Product = temp;
                        CountingSumaOrder();
                    }
                    else
                    {
                        temp = new ObservableCollection<ProductForSaleModel>();
                        temp = Product;


                        temp.ElementAt(Product.Count - 1).Count -= 1;
                        Product = new ObservableCollection<ProductForSaleModel>();
                        Product = temp;
                        CountingSumaOrder();
                    }
                    BarCodeSearch = string.Empty;
                }
            }
        }

        private void CountingSumaOrder()
        {
            SumaOrder = 0;
            foreach (ProductForSaleModel orderProduct in Product)
            {
                SumaOrder += (orderProduct.Product.Price * orderProduct.Count);
            }
            _totalSum = SumaOrder.Value;
            if (DiscountPrecent != 0)
            {
                SumaOrder = SumaOrder - (SumaOrder * (DiscountPrecent / 100));
            }
            if (Discount != 0)
            {
                SumaOrder = SumaOrder - Discount;
            }
            if (SumaOrder < 0)
            {
                SumaOrder = _totalSum;
                _discount = 0;
                _discountPrecent = 0;
            }
            EnableButton();
        }

        private void EnableButton()
        {
            if (Product.Count <= 0)
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
                Product.Remove(product);
                CountingSumaOrder();
            }
        }
        public ICommand AddNumberToTextBoxCommand { get => CreateCommandParameter<object>(AddNumberToTextBoxField); }
        private void AddNumberToTextBoxField(object parameter)
        {
            if (parameter != null)
            {
                SumaUser += Convert.ToInt32(parameter);
            }
        }

        public ICommand ClearSumUserCommand => _cleareSumUserCommand;
        private void ClearSumUser()
        {
            SumaUser = 0;
        }
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        private async Task PrintingCheck()
        {
            try
            {
                IsEnableSendCheckButton = false;
                _workingShiftService.LoadSaleMenuDataFromFile();
                _saleMenuService.IsDrawinfChek = DrawingCheck;

                if (!(SumaUser >= SumaOrder))
                {
                    throw new Exception("Сума внеску не може бути менша ніж сума чеку");
                }
                else
                {
                    _saleMenuService.AddKey(_user.SignatureKey);


                    var rest = (SumaUser - SumaOrder);
                    var discount = new Discount();

                    if (DiscountPrecent != 0)
                    {
                        discount.TotalDiscount = _totalSum * (DiscountPrecent / 100);
                        discount.TypeDiscount = 1;
                        discount.CreateAt = DateTime.Now;
                        discount.InterimAmount = _totalSum;
                        discount.Rebate = DiscountPrecent;
                    }
                    else if (Discount != 0)
                    {
                        discount.TotalDiscount = Discount;
                        discount.TypeDiscount = 0;
                        discount.CreateAt = DateTime.Now;
                        discount.InterimAmount = _totalSum;
                        discount.Rebate = Discount;
                    }
                    else
                    {
                        discount = null;
                    }


                    Operation operation = new Operation()
                    {
                        TypeOperation = (Model.Enum.TypeOperation)TypeOperation.FiscalCheck,
                        MAC = await _saleMenuService.GetMAC(),
                        CreatedAt = DateTime.Now,
                        NumberPayment = await _saleMenuService.GetLocalNumber(),
                        GoodsTax = "0",
                        RestPayment = rest.Value,
                        TotalPayment = _totalSum,
                        BuyersAmount = SumaUser.Value,
                        TypePayment = (Model.Enum.TypePayment)(TypePayment)SelectTypePayment,
                        Discount = discount,
                    };

                    if (await _saleMenuService.SendCheck(Product, operation))

                    {
                        MessageBox.Show("Решта: " + rest, "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                        Product = new ObservableCollection<ProductForSaleModel>();
                        BarCodeSearch = string.Empty;
                        SumaUser = new decimal();
                        SumaUser = 0;
                        SumaOrder = 0;
                        Discount = 0;
                    }
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
            try
            {
                IsEnableSendCheckButton = false;
                _workingShiftService.LoadSaleMenuDataFromFile();
                _saleMenuService.IsDrawinfChek = DrawingCheck;

                if (!(SumaUser >= SumaOrder))
                {
                    MessageBox.Show("Сума внеску не може бути менша ніж сума чеку");
                }
                else
                {
                    _saleMenuService.AddKey(_user.SignatureKey);

                    var rest = (SumaUser - SumaOrder);
                    var discount = new Discount();

                    if (DiscountPrecent != 0)
                    {
                        discount.TotalDiscount = _totalSum * (DiscountPrecent / 100);
                        discount.TypeDiscount = 1;
                        discount.CreateAt = DateTime.Now;
                        discount.InterimAmount = _totalSum;
                        discount.Rebate = DiscountPrecent;
                    }
                    else if (Discount != 0)
                    {
                        discount.TotalDiscount = Discount;
                        discount.TypeDiscount = 0;
                        discount.CreateAt = DateTime.Now;
                        discount.InterimAmount = _totalSum;
                        discount.Rebate = Discount;
                    }
                    else
                    {
                        discount = null;
                    }

                    Operation operation = new Operation()
                    {
                        TypeOperation = (Model.Enum.TypeOperation)TypeOperation.ReturnCheck,
                        MAC = await _saleMenuService.GetMAC(),
                        CreatedAt = DateTime.Now,
                        NumberPayment = await _saleMenuService.GetLocalNumber(),
                        GoodsTax = "0",
                        RestPayment = rest.Value,
                        TotalPayment = _totalSum,
                        BuyersAmount = SumaUser.Value,
                        TypePayment = (Model.Enum.TypePayment)(TypePayment)SelectTypePayment,
                        Discount = discount,
                    };

                    if (await _saleMenuService.SendCheck(Product, operation))

                    {
                        MessageBox.Show("Товар повернено", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        Product = new ObservableCollection<ProductForSaleModel>();
                        BarCodeSearch = string.Empty;
                        SumaUser = new decimal();
                        SumaUser = 0;
                        SumaOrder = 0; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
    }
}
