using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Views.ToolsPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class StorageViewModel : ViewModel<StorageViewModel>
    {
        private StorageModel? _model;

        private ICommand _searchCommand;
        private ICommand _visibileAllCommand;
        private ICommand _openCreateGoodsWindowCommand;
        private ICommand _openFormationGoodsWindowCommand;

        private List<Goods> _goods;
        
        public StorageViewModel()
        {
            GoodsList = new List<Goods>();
            SearchTemplateName = new List<string>();

            _searchCommand = new DelegateCommand(SearchGoodsInCodeAndName);
            _visibileAllCommand = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start(); });
            _openCreateGoodsWindowCommand = new DelegateCommand(() => { new CreateGoodsPage().Show(); });
            _openFormationGoodsWindowCommand = new DelegateCommand(() => { new FormationProduct().Show(); });

            SizeDataGrid = (double)SystemParameters.PrimaryScreenWidth;
            _goods = new List<Goods>();

            _nameSearch = string.Empty;
            _searchTemplateName = new List<string>();

            SetFieldTextComboBox();

            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
        }

        private List<Goods>? _goodslist;
        public List<Goods>? GoodsList
        {
            get { return _goodslist; }
            set{ _goodslist = value; OnPropertyChanged("GoodsList"); }
        }

        private double _sizeDataGrid;
        public double SizeDataGrid
        {
            set{ _sizeDataGrid = value; OnPropertyChanged("SizeDataGrid"); }
        }

        private List<string> _searchTemplateName;
        public List<string> SearchTemplateName
        {
            get { return _searchTemplateName; }
            set { _searchTemplateName = value; OnPropertyChanged("SearchTemplateName"); }
        }

        private int _selectedIndexSearch;
        public int SelectedIndexSearch
        {
            get { return _selectedIndexSearch; }
            set { _selectedIndexSearch = value; OnPropertyChanged("SelectedIndexSearch"); }
        }

        private string _nameSearch;
        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged("NameSearch"); }
        }

        private void SetFieldTextComboBox()
        {
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        public ICommand SearchCommand => _searchCommand;

        void SearchGoodsInCodeAndName()
        {
            if (_model != null)
                switch (_selectedIndexSearch)
                {
                    case 0:
                        {
                            GoodsList.Clear();
                            GoodsList = _model.SearchGoods(_nameSearch, TypeSearch.Code);
                            break;
                        }
                    case 1:
                        {
                            GoodsList.Clear();
                            GoodsList = _model.SearchGoods(_nameSearch, TypeSearch.Name);
                            break;
                        }
                    case 2:
                        {
                            GoodsList.Clear();
                            GoodsList = _model.SearchGoods(_nameSearch, TypeSearch.Articule);
                            break;
                        }
                }
        }
        public ICommand VisibileAllCommand => _visibileAllCommand;

        void SetFieldItemDataGridThread()
        {
            GoodsList.Clear();
            _model = new StorageModel();
            GoodsList = _model.GetItems();
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
                if(NameSearch!=string.Empty) 
                {
                    SearchGoodsInCodeAndName();
                }
            }
            else
            {
                StaticResourse.goodsList = _goods;
                new UpdateGoodsRange().ShowDialog();
                if(NameSearch !=string.Empty)
                {
                    SearchGoodsInCodeAndName();
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
                                if (NameSearch != string.Empty)
                                {
                                    SearchGoodsInCodeAndName();
                                }
                                else
                                {
                                    new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();

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
                            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
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
                            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
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
