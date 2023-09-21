using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.MiniServiceSigningFile;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.SalePage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.SalePage
{
    internal class SaleMenuViewModel : ViewModel<SaleMenuViewModel>
    {
        private SaleMenuModel _model;

        private ICommand _searchBarCodeGoodsCommand;
        private ICommand _printingCheckCommand;


        private ICommand _openChangeCommand;
        private ICommand _closeChangeCommand;


        public SaleMenuViewModel() 
        {
            _model = new SaleMenuModel();
      
            _searchBarCodeGoodsCommand = new DelegateCommand(SearchBarCodeGoods);
            _printingCheckCommand = new DelegateCommand(PrintingCheck);

            _openChangeCommand = new DelegateCommand(() => { OpenChange(); });

            _closeChangeCommand = new DelegateCommand(() => { _model.CloseChange(new Operation
            {
                dataPacketIdentifier = 1,
                typeRRO = 0,
                fiscalNumberRRO = AppSettingsManager.GetParameterFiles("FiscalNumberRRO").ToString(),
                taxNumber = AppSettingsManager.GetParameterFiles("TaxNumber").ToString(),
                factoryNumberRRO = "v1",
                mac = _model.GetMac(true),
                createdAt = DateTime.Now,
                numberPayment = _model.GetLocalNumber(),
                numberOfSalesReceipts = Convert.ToDecimal(_model.GetLocalNumber()),

            }); });
            
            _products = new List<Goods>();
            _barCodeSearch = string.Empty;
            _sumaOrder = 0;
            _sumaUser = 0;
            _selectIndex = 0;

            _typeOplatu = new List<string>();
            _typeOplatu.Add("готівкою");
            _typeOplatu.Add("безготівкові форми оплати");
        }
        public ICommand CloseChangeCommand => _closeChangeCommand;

        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged("BarCodeSearch"); }
        }

        private List<Goods> _products;
        public List<Goods> Products 
        {
            get {  return _products; }
            set { _products = value; OnPropertyChanged("Products");}
        }

        private decimal? _sumaOrder;
        public decimal? SumaOrder
        {
            get { return _sumaOrder; }
            set { _sumaOrder = value;OnPropertyChanged("SumaOrder"); }
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
            set { _selectIndex = value;OnPropertyChanged("_selectIndex"); }
        }

        public ICommand SearchBarCodeGoodsCommand => _searchBarCodeGoodsCommand;

        private void SearchBarCodeGoods()
        {
            List<Goods> temp;
            if (BarCodeSearch.Length > 12)
            {
                if (BarCodeSearch != "2")
                {
                    var item = _model.Search(BarCodeSearch);
                    if (item != null)
                    {
                        item.count = 1;
                        temp = new List<Goods>();
                        temp = Products;

                        if (temp.Find(pr => pr.code == item.code) != null)
                        {
                            temp.Find(pr => pr.code == item.code).count += 1;
                        }
                        else
                        {
                            temp.Add(item);
                        }

                        CountingSumaOrder(temp);

                        Products = new List<Goods>();
                        Products = temp;
                        BarCodeSearch = string.Empty;
                    }
                }
                else
                {
                    if (Products.Count() != 0)
                    {
                        if (Products.ElementAt(Products.Count - 1).count == 1)
                        {
                            temp = new List<Goods>();
                            temp = Products;

                            temp.Remove(temp.ElementAt(temp.Count - 1));
                            Products = new List<Goods>();
                            Products = temp;
                            CountingSumaOrder(Products);
                        }
                        else
                        {
                            temp = new List<Goods>();
                            temp = Products;


                            temp.ElementAt(Products.Count - 1).count -= 1;
                            Products = new List<Goods>();
                            Products = temp;
                            CountingSumaOrder(Products);
                        }
                        BarCodeSearch = string.Empty;
                    }
                }
            }
        }

        private void CountingSumaOrder(List<Goods> products)
        {
            SumaOrder = 0;
            foreach (Goods orderProduct in products)
            {
                SumaOrder += (orderProduct.price * orderProduct.count);
            }
        }
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        private void PrintingCheck()
        {
            if (SumaUser >= SumaOrder)
            {
                double rest = ((double)(SumaUser - SumaOrder));

                if(_model.SendChek(Products, new Operation()
                {
                    dataPacketIdentifier = 1,
                    typeRRO = 0,
                    fiscalNumberRRO = AppSettingsManager.GetParameterFiles("FiscalNumberRRO").ToString(),
                    taxNumber = AppSettingsManager.GetParameterFiles("TaxNumber").ToString(),
                    factoryNumberRRO = "v1",
                    typeOperation = 0,
                    mac = _model.GetMac(true),
                    createdAt = DateTime.Now,
                    numberPayment = _model.GetLocalNumber(),
                    goodsTax = "0",
                    restPayment = Convert.ToDecimal(rest),
                    totalPayment = (decimal)SumaOrder,
                    buyersAmount = (decimal)SumaUser,
                    formOfPayment = SelectIndex,
                }))
                {
                    MessageBox.Show("Решта: " + rest, "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                    Products = new List<Goods>();
                    BarCodeSearch = string.Empty;
                    SumaUser = new decimal();
                    SumaUser = 0;
                    SumaOrder = 0;
                }
                
            }
            else
            {
                MessageBox.Show("Сума внеску неможе бути менша ніж сума чеку");
            }
        }

        public ICommand OpenChangeCommand => _openChangeCommand;
        private void OpenChange()
        {
            Operation operation = new Operation
            {
                dataPacketIdentifier = 1,
                typeRRO = 0,
                fiscalNumberRRO = AppSettingsManager.GetParameterFiles("FiscalNumberRRO").ToString(),
                taxNumber = AppSettingsManager.GetParameterFiles("TaxNumber").ToString(),
                factoryNumberRRO = "v1",
                typeOperation = 108,
                createdAt = DateTime.Now,
                numberPayment = "0",
                mac = _model.GetMac(false),
            };
          
            _model.OpenChange(operation, true);
        }

    }
}
