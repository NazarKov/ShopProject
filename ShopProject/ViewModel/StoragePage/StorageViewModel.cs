using Org.BouncyCastle.Asn1.X509;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Views.ToolsPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace ShopProject.ViewModel.StoragePage
{
    internal class StorageViewModel : ViewModel<StorageViewModel>
    {
        private static ManualResetEventSlim _eventSlim = new ManualResetEventSlim(true);

        private StorageModel? _model;
        private string _nameSearch;

        private ICommand _openCreateGoodsWindowCommand;
        private ICommand _openFormationGoodsWindowCommand;
        private ICommand _updateSizeGridCommand;

        private List<Goods> _goods;

        public StorageViewModel()
        {
            _openCreateGoodsWindowCommand = new DelegateCommand(() => { new CreateGoodsPage().Show(); });
            _openFormationGoodsWindowCommand = new DelegateCommand(() => { new FormationGoods().Show(); });
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);

            _model = new StorageModel();
            _goods = new List<Goods>();
            
            _nameSearch = string.Empty;
            _statusBarCountGoods = string.Empty;
            
            GoodsList = new List<Goods>();

            SearchGoods("");
        }
        public ICommand UpdateSize => _updateSizeGridCommand;

        private List<Goods>? _goodslist;
        public List<Goods>? GoodsList
        {
            get { return _goodslist; }
            set { _goodslist = value; OnPropertyChanged("GoodsList"); }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged("Width"); }
        }

        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged("Heigth"); }
        }


        private string _statusBarCountGoods;
        public string StatusBarCountGoods
        {
            get { return _statusBarCountGoods; }
            set { _statusBarCountGoods = value; OnPropertyChanged("StatusBarCountGoods"); }
        }

        private void UpdateSizes()
        {
            Width = (int)Application.Current.MainWindow.ActualWidth;
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 280;
        }

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchGoods, CanRegister); }

        private void SearchGoods(object parameter)
        {
            
            _nameSearch = parameter.ToString();
            new Thread(() =>
            {  
                if (GoodsList.Count != 0)
                {
                    GoodsList.Clear();
                }
                GoodsList = _model.SearchGoods(parameter.ToString());
                StatusBarCountGoods = "Кілкісь товарі: " + GoodsList.Count;
            }).Start();
            _eventSlim.Wait();
            _eventSlim.Set();
        }
  

        public ICommand UpdateGoodsCommand { get => new DelegateParameterCommand(UpdateGoods, CanRegister); }
        private void UpdateGoods(object parameter)
        {
            _goods = new List<Goods>();
            if (_model != null)
                _model.ContertToListGoods((IList)parameter, _goods);
                
            if (_goods.Count == 1)
            {
                StaticResourse.goods = _goods[0];
                new UpdateGoods().ShowDialog();
                if(_nameSearch!=string.Empty) 
                {
                    SearchGoods(_nameSearch);
                }
            }
            else
            {
                StaticResourse.goodsList = _goods;
                new UpdateGoodsRange().ShowDialog();
                if (_nameSearch != string.Empty)
                {
                    SearchGoods(_nameSearch);
                }
            }
            
        }

        public ICommand DeleteGoodsCommand { get => new DelegateParameterCommand(DeleteGoods, CanRegister); }
        private void DeleteGoods(object parameter)
        {
            if (MessageBox.Show("видалити?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goods = new List<Goods>();
                if (_model != null)
                    _model.ContertToListGoods((IList)parameter, _goods);
                if (_goods.Count == 1)
                {
                    if (_goods[0] != null)
                        if (_model != null)
                        {
                            if (_model.DeleteGoods(_goods[0]))
                            {
                                if (_nameSearch != string.Empty)
                                {
                                    SearchGoods(_nameSearch);
                                }
                                else
                                {
                                    SearchGoods(_nameSearch);
                                }
                            }
                        }
                }
            }
        }

        public ICommand AddGoodsArhiveCommand { get => new DelegateParameterCommand(AddGoodsArhive, CanRegister); }
        private void AddGoodsArhive(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goods = new List<Goods>();
                if (_model != null)
                {
                    _model.ContertToListGoods((IList)parameter, _goods);
                    if (_goods.Count == 1)
                    {
                        if(_model.SetGoodsInArhive(_goods[0]))
                        {
                            if(_nameSearch != string.Empty)
                            {
                                SearchGoods(_nameSearch);
                            }
                            else
                            {
                                SearchGoods(_nameSearch);
                            }
                        }
                    }
                }
            }
        }
        
        public ICommand AddOutOfStockGoodsCommand { get => new DelegateParameterCommand(AddOutOfStockGoods, CanRegister); }
        private void AddOutOfStockGoods(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goods = new List<Goods>();
                if (_model != null)
                {
                    _model.ContertToListGoods((IList)parameter, _goods);
                    if (_goods.Count == 1)
                    {
                        if (_model.SetGoodsOutOfStok(_goods[0]))
                        {
                            if (_nameSearch != string.Empty)
                            {
                                SearchGoods(_nameSearch);
                            }
                            else
                            {
                                SearchGoods(_nameSearch);
                            }
                        }
                    }
                }
            }
        }
        
        public ICommand OpenWindoiwCreateStikerCommand{ get => new DelegateParameterCommand(ShowWindowCreateStikerCommand, CanRegister); }
        private void ShowWindowCreateStikerCommand(object parameter)
        {
            _goods = new List<Goods>();
            if (_model != null)
                _model.ContertToListGoods((IList)parameter, _goods);

            if (_goods.Count == 1)
            {
                StaticResourse.goods = _goods[0];
                new CreateStiker().Show();
            }
        }
        
        private bool CanRegister(object parameter) => true;

        public ICommand OpenCreateGoodsWindowCommand => _openCreateGoodsWindowCommand;
        public ICommand OpenFormationGoodsWindowCommand => _openFormationGoodsWindowCommand;

    }
}
