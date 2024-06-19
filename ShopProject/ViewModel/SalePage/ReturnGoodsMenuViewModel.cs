using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.SalePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace ShopProject.ViewModel.SalePage
{
    internal class ReturnGoodsMenuViewModel : ViewModel<ReturnGoodsMenuViewModel>
    {
        private ReturnGoodsMenuModel _model;


        private ICommand _searchBarCodeGoodsCommand;
        private ICommand _printingCheckCommand;
        private ICommand _clearFieldDataGrid;

        private ICommand _updateSize;

        public ReturnGoodsMenuViewModel()
        {
            _model = new ReturnGoodsMenuModel();


            _searchBarCodeGoodsCommand = new DelegateCommand(SearchBarCodeGoods);
            _printingCheckCommand = new DelegateCommand(PrintingCheck);
            _updateSize = new DelegateCommand(UpdateSizes);
            _clearFieldDataGrid = new DelegateCommand(ClearField);

            _goods = new List<ProductEntiti>();
            _barCodeSearch = string.Empty;

            _typeOplatu = new List<string>();
            _typeOplatu.Add("готівкою");
            _typeOplatu.Add("безготівкові форми оплати");

            ClearField();
        }
        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged("BarCodeSearch"); }
        }

        private List<ProductEntiti> _goods;
        public List<ProductEntiti> Goods
        {
            get { return _goods; }
            set { _goods = value; OnPropertyChanged("Goods"); }
        }

        private decimal? _sumaOrder;
        public decimal? SumaOrder
        {
            get { return _sumaOrder; }
            set { _sumaOrder = value; OnPropertyChanged("SumaOrder"); }
        }

        private decimal? _sumaUser;
        public decimal? SumaUser
        {
            get { return _sumaUser; }
            set { _sumaUser = value; OnPropertyChanged("SumaUser"); }
        }
        private List<string> _typeOplatu;
        public List<string> TypeOplatu
        {
            get { return _typeOplatu; }
            set { _typeOplatu = value; OnPropertyChanged("_typeOplatu"); }
        }
        private int _selectIndex;
        public int SelectIndex
        {
            get { return _selectIndex; }
            set { _selectIndex = value; OnPropertyChanged("_selectIndex"); }
        }

        public ICommand UpdateSize => _updateSize;

        private int _widght;
        public int Widght
        {
            get { return _widght; }
            set { _widght = value; OnPropertyChanged("Widght"); }
        }


        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }

        private void UpdateSizes()
        {
            Widght = (int)Application.Current.MainWindow.ActualWidth - 175;
            Height = (int)Application.Current.MainWindow.ActualHeight - 290;
        }
        public ICommand ClearFieldDataGid => _clearFieldDataGrid;
        private void ClearField()
        {
            Goods = new List<ProductEntiti>();
            SumaUser = 0;
            SumaOrder = 0;
            _selectIndex = 0;
        }

        public int Tag;

        public ICommand SearchBarCodeGoodsCommand => _searchBarCodeGoodsCommand;

        private void SearchBarCodeGoods()
        {
            List<ProductEntiti> temp;
            if (BarCodeSearch.Length > 12)
            {
                if (BarCodeSearch != "2")
                {
                    var item = _model.Search(BarCodeSearch);
                    if (item != null)
                    {
                        item.Count = 1;
                        temp = new List<ProductEntiti>();
                        temp = Goods;

                        if (temp.Find(pr => pr.Code == item.Code) != null)
                        {
                            temp.Find(pr => pr.Code == item.Code).Count += 1;
                        }
                        else
                        {
                            temp.Add(item);
                        }

                        CountingSumaOrder(temp);

                        Goods = new List<ProductEntiti>();
                        Goods = temp;
                        BarCodeSearch = string.Empty;
                    }
                }
                else
                {
                    if (Goods.Count() != 0)
                    {
                        if (Goods.ElementAt(Goods.Count - 1).Count == 1)
                        {
                            temp = new List<ProductEntiti>();
                            temp = Goods;

                            temp.Remove(temp.ElementAt(temp.Count - 1));
                            Goods = new List<ProductEntiti>();
                            Goods = temp;
                            CountingSumaOrder(Goods);
                        }
                        else
                        {
                            temp = new List<ProductEntiti>();
                            temp = Goods;


                            temp.ElementAt(Goods.Count - 1).Count -= 1;
                            Goods = new List<ProductEntiti>();
                            Goods = temp;
                            CountingSumaOrder(Goods);
                        }
                        BarCodeSearch = string.Empty;
                    }
                }
            }
        }

        private void CountingSumaOrder(List<ProductEntiti> products)
        {
            SumaOrder = 0;
            foreach (ProductEntiti orderProduct in products)
            {
                SumaOrder += (orderProduct.Price * orderProduct.Count);
            }
        }
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        private void PrintingCheck()
        {

            if (SumaUser >= SumaOrder)
            {
                double rest = ((double)(SumaUser - SumaOrder));




                if (_model.SendCheck(Goods, new OperationEntiti()
                {
                    DataPacketIdentifier = 1,
                    TypeRRO = 0,
                    FiscalNumberRRO = AppSettingsManager.GetParameterFiles("FiscalNumberRRO").ToString(),
                    TaxNumber = AppSettingsManager.GetParameterFiles("TaxNumber").ToString(),
                    FactoryNumberRRO = "v1",
                    TypeOperation = 1,
                    MAC = _model.GetMac(),
                    CreatedAt = DateTime.Now,
                    NumberPayment = _model.GetLocalNumber(),
                    GoodsTax = "0",
                    RestPayment = Convert.ToDecimal(rest),
                    TotalPayment = (decimal)SumaOrder,
                    BuyersAmount = (decimal)SumaUser,
                    FormOfPayment = SelectIndex,
                }))
                {
                    MessageBox.Show("Решта: " + rest, "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                    Goods = new List<ProductEntiti>();
                    BarCodeSearch = string.Empty;
                    SumaUser = new decimal();
                    SumaUser = 0;
                    SumaOrder = 0;

                    if (Tag != 0)
                    {
                        //StaticResourse.tabs.RemoveAt(Tag);
                    }
                }

            }
            else
            {
                MessageBox.Show("Сума внеску не може бути менша ніж сума чеку");
            }
        }

    }
}
