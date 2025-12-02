using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Model.Command;
using ShopProject.Model.SalePage;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.SalePage
{
    internal class SaleGoodsMenuViewModel : ViewModel<SaleGoodsMenuViewModel>
    {
        private SaleGoodsMenuModel _model;

        private ICommand _searchBarCodeGoodsCommand;
        private ICommand _printingCheckCommand;
        private ICommand _clearFieldDataGrid; 
        private ICommand _updateSize;
        private ICommand _cleareSumUserCommand;
        private Guid _idChannel;

        public SaleGoodsMenuViewModel() 
        {
            _model = new SaleGoodsMenuModel();

            _searchBarCodeGoodsCommand = new DelegateCommand(async () => { await SearchBarCodeGoods(); });
            _printingCheckCommand = new DelegateCommand(PrintingCheck);
            _updateSize = new DelegateCommand(UpdateSizes);
            _clearFieldDataGrid = new DelegateCommand(ClearField);
            _cleareSumUserCommand = new DelegateCommand(ClearSumUser);

            _product = new ObservableCollection<ProductForSale>();
            _barCodeSearch = string.Empty;

            _typeOplatu = new List<string>();
            _typeOplatu.Add("готівкою");
            _typeOplatu.Add("безготівкові форми оплати");

            //DrawingCheck = _model.IsDrawinfChek;
            IsFiscalCheck = true;

            ClearField();
        }

        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged(nameof(BarCodeSearch)); }
        }

        private ObservableCollection<ProductForSale> _product;
        public ObservableCollection<ProductForSale> Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        private decimal? _sumaOrder;
        public decimal? SumaOrder
        {
            get { return _sumaOrder; }
            set { _sumaOrder = value;OnPropertyChanged(nameof(SumaOrder)); }
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
        private int _selectIndex;
        public int SelectIndex
        {
            get { return _selectIndex; }
            set { _selectIndex = value;OnPropertyChanged(nameof(SelectIndex)); }
        }

        public ICommand UpdateSize => _updateSize;

        private int _widght;
        public int Widght
        {
            get { return _widght; }
            set { _widght = value;OnPropertyChanged(nameof(Widght)); }
        }


        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged(nameof(Height)); }
        }

        private void UpdateSizes()
        {
            Widght = (int)Application.Current.MainWindow.ActualWidth - 530;
            Height = (int)Application.Current.MainWindow.ActualHeight-220;
        }
        public ICommand ClearFieldDataGid => _clearFieldDataGrid;
        private void ClearField()
        {
            Product = new ObservableCollection<ProductForSale>();
            SumaUser = 0;
            SumaOrder = 0;
            _selectIndex = 0;
             
            _idChannel = Guid.NewGuid();
            MediatorService.AddEvent(NavigationButton.CountingSumaOrder.ToString() + "" + _idChannel, this.CountingSumaOrder);
            MediatorService.AddEvent(NavigationButton.RemoveProduct.ToString() + "" + _idChannel, this.RemoveItem);
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
        public int Tag;

        public ICommand SearchBarCodeGoodsCommand => _searchBarCodeGoodsCommand;

        private async Task SearchBarCodeGoods()
        {
            ObservableCollection<ProductForSale> temp;
            if (BarCodeSearch.Length > 12)
            {
                if (BarCodeSearch != "2")
                {
                    var item = await _model.Search(BarCodeSearch);
                    if (item != null)
                    {
                        
                        item.Count = 1;
                        temp = new ObservableCollection<ProductForSale>();
                        temp = Product; 

                        if (temp.FirstOrDefault(pr => pr.Product.Code == item.Code) != null)
                        {
                            temp.FirstOrDefault(pr => pr.Product.Code == item.Code).Count += 1;
                        }
                        else
                        {
                            temp.Add(new ProductForSale(item) {Cannnel = _idChannel });
                        }

                        CountingSumaOrder();

                        Product = new ObservableCollection<ProductForSale>();
                        Product = temp;
                    }
                }
                else
                {
                    if (Product.Count() != 0)
                    {
                        if (Product.ElementAt(Product.Count - 1).Count == 1)
                        {
                            temp = new ObservableCollection<ProductForSale>();
                            temp = Product;

                            temp.Remove(temp.ElementAt(temp.Count - 1));
                            Product = new ObservableCollection<ProductForSale>();
                            Product = temp;
                            CountingSumaOrder();
                        }
                        else
                        {
                            temp = new ObservableCollection<ProductForSale>();
                            temp = Product;


                            temp.ElementAt(Product.Count - 1).Count -= 1;
                            Product = new ObservableCollection<ProductForSale>();
                            Product = temp;
                            CountingSumaOrder();
                        }
                    }
                }
                BarCodeSearch = string.Empty;
            } 
        }

        private void CountingSumaOrder(object obj = null)
        {
            SumaOrder = 0;
            foreach (ProductForSale orderProduct in Product)
            {
                SumaOrder += (orderProduct.Product.Price * orderProduct.Count);
            }
        }
        private void RemoveItem(object item)
        {
            var product = item as ProductForSale;
            if (product != null) 
            {
                Product.Remove(product);
                CountingSumaOrder();
            }
        }
        public ICommand AddNumberToTextBoxCommand { get => new DelegateParameterCommand(AddNumberToTextBoxField, CanRegister); }
        private void AddNumberToTextBoxField(object parameter)
        {
            if (parameter!=null)
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
        private void PrintingCheck()
        {
            //_model.IsDrawinfChek = DrawingCheck;

            if (SumaUser >= SumaOrder)
            {
                double rest = ((double)(SumaUser - SumaOrder));


                //decimal typeOperration;
                //if (IsFiscalCheck)
                //{
                //    typeOperration = 0;
                //}
                //else
                //{
                //    typeOperration = 200;
                //}

                Task t = Task.Run(async () =>{

                    Operation operation = new Operation()
                    { 
                        TypeOperation =  TypeOperation.FiscalCheck,
                        MAC = await _model.GetMAC(Session.FocusDevices.ID),
                        CreatedAt = DateTime.Now,
                        NumberPayment = await _model.GetLocalNumber(),
                        GoodsTax = "0",
                        RestPayment = Convert.ToDecimal(rest),
                        TotalPayment = (decimal)SumaOrder,
                        BuyersAmount = (decimal)SumaUser,
                        TypePayment =  TypePayment.Cash,  
                    };

                    if (_model.SendCheck(Product, operation))
                    
                    {
                        MessageBox.Show("Решта: " + rest, "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                        Product = new ObservableCollection<ProductForSale>();
                        BarCodeSearch = string.Empty;
                        SumaUser = new decimal();
                        SumaUser = 0;
                        SumaOrder = 0;

                        if (Tag != 0)
                        {
                            Session.Tabs.RemoveAt(Tag);
                        }
                    }
                }); 
            }
            else
            {
                MessageBox.Show("Сума внеску не може бути менша ніж сума чеку");
            }
        }
        private bool CanRegister(object parameter) => true;


    }
}
