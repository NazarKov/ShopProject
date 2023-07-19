using ShopProject.DataBase.Model;
using ShopProject.Helpers.DFSAPI;
using ShopProject.Helpers.MiniServiceSigningFile;
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

        private ICommand _searchBarCodeProdcutCommand;
        private ICommand _printingCheckCommand;


        private ICommand _openChangeCommand;
        private ICommand _closeChangeCommand;


        public SaleMenuViewModel() 
        {
            _model = new SaleMenuModel();
      
            _searchBarCodeProdcutCommand = new DelegateCommand(SearchBarCodePrdocut);
            _printingCheckCommand = new DelegateCommand(PrintingCheck);

            _openChangeCommand = new DelegateCommand(() => { _model.OpenChange(); });
            _closeChangeCommand = new DelegateCommand(() => { _model.closeChange(); });
            
            _products = new List<Product>();
            _barCodeSearch = string.Empty;
            _sumaOrder = 0;
            _sumaUser = 0;
            _selectIndex = 0;

            _typeOplatu = new List<string>();
            _typeOplatu.Add("готівкою");
            _typeOplatu.Add("безготівкові форми оплати");
        }

        public ICommand OpenChangeCommand => _openChangeCommand;
        public ICommand CloseChangeCommand => _closeChangeCommand;

        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged("BarCodeSearch"); }
        }

        private List<Product> _products;
        public List<Product> Products 
        {
            get {  return _products; }
            set { _products = value; OnPropertyChanged("Products");}
        }

        private double? _sumaOrder;
        public double? SumaOrder
        {
            get { return _sumaOrder; }
            set { _sumaOrder = value;OnPropertyChanged("SumaOrder"); }
        }

        private double? _sumaUser;
        public double? SumaUser
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

        public ICommand SearchBarCodeProductCommand => _searchBarCodeProdcutCommand;

        private void SearchBarCodePrdocut()
        {
            List<Product> temp;
            if (BarCodeSearch != "0000000000000")
            {
                if (BarCodeSearch.Length > 12)
                {
                    var item = _model.Search(BarCodeSearch);
                    if (item != null)
                    {
                        item.count = 1;
                        temp = new List<Product>();
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

                        Products = new List<Product>();
                        Products = temp;
                        BarCodeSearch = string.Empty;
                    }
                }
            }
            else
            {
                if (Products.ElementAt(Products.Count - 1).count == 1)
                {
                    temp = new List<Product>();
                    temp = Products;

                    temp.Remove(temp.ElementAt(temp.Count - 1));
                    Products = new List<Product>();
                    Products = temp;
                    CountingSumaOrder(Products);
                }
                else
                {
                    temp = new List<Product>();
                    temp = Products;


                    temp.ElementAt(Products.Count - 1).count -= 1;
                    Products = new List<Product>();
                    Products = temp;
                    CountingSumaOrder(Products);
                }
                BarCodeSearch = string.Empty;
            }
           
        }

        private void CountingSumaOrder(List<Product> products)
        {
            SumaOrder = 0;
            foreach (Product orderProduct in products)
            {
                SumaOrder += (orderProduct.price * orderProduct.count);
            }
        }
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        private void PrintingCheck()
        {
            DateTime time = DateTime.Now;

            double rest = ((double)(SumaUser - SumaOrder));
            Order order = new Order() { created_at = time, sale = 0, suma = (double)SumaOrder, rest = rest, user = null, LocalNumber = "0", userSuma = (double)SumaUser, type_oplat = TypeOplatu.ElementAt(SelectIndex) };
            if (_model.SetOrderDataBase(Products, order))
            {
                //Messe mes = new Messe() { id = "123", mac = "123" }; 
                Messe mes = _model.SendChek(Products,order,time);
                
                _model.PrintChek(Products,order,mes,time);
                MessageBox.Show($"чек видано \n Решта:{rest}");
                Products = new List<Product>();
                BarCodeSearch = string.Empty;
                SumaUser = new double();
                SumaUser = 0;
                SumaOrder = 0;

            }
        }


    }
}
