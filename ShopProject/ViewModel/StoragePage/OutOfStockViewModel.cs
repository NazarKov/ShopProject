using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Views.StoragePage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class OutOfStockViewModel : ViewModel<OutOfStockViewModel>
    {
        private OutOfStockModel _model;

        private ICommand _searchCommand;
        private ICommand _visibileAllCommand;
        private List<Goods> _goodsList;

        public OutOfStockViewModel() 
        {
            _model = new OutOfStockModel();
            _goods = new List<Goods>();
            _goodsList = new List<Goods>();

            _visibileAllCommand = new DelegateCommand(setFieldDataGrid);
            _searchCommand = new DelegateCommand(SearchProductInCodeAndName);
            setFieldDataGrid();
            SetFieldTextComboBox();
        }
        private void setFieldDataGrid()
        {
            Goods = _model.GetAll();   
        }

        private void SetFieldTextComboBox()
        {
            SearchTemplateName = new List<string>();
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        private List<Goods> _goods;
        public List<Goods> Goods
        {
            get { return _goods; }
            set { _goods = value; OnPropertyChanged("Goods"); }
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

        public ICommand SearchCommand => _searchCommand;

        void SearchProductInCodeAndName()
        {
            if (_model != null)
                switch (_selectedIndexSearch)
                {
                    case 0:
                        {
                            Goods = _model.SearchGoods(_nameSearch);
                            break;
                        }
                    case 1:
                        {
                            Goods = _model.SearchGoods(_nameSearch);
                            break;
                        }
                    case 2:
                        {
                            Goods = _model.SearchGoods(_nameSearch);
                            break;
                        }
                }
        }
        public ICommand VisibileAllCommand => _visibileAllCommand;

        public ICommand ReturnGoodsInStorageCommand { get => new DelegateParameterCommand(ReturnGoodsInStorage, (object parameter) => true); }
        private void ReturnGoodsInStorage(object parameter)
        {
            if (MessageBox.Show("Перенести", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goodsList = new List<Goods>();
                if (_goodsList != null)
                {
                    _model.ConvertToList((IList)parameter, _goodsList);
                    if (_goodsList.Count == 1)
                    {
                        if (_model.ReturnGoodsInStorage(_goodsList[0]))
                        {
                            new Thread(new ThreadStart(setFieldDataGrid)).Start();
                        }
                    }
                }
            }
        }

        public ICommand DeleteGoodsOutOfStockCommand { get => new DelegateParameterCommand(DeleteGoods, (object parameter) => true); }
        private void DeleteGoods(object parameter)
        {
            if (MessageBox.Show("Ви точно хочете видалити?\nТовар також видаляється.", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goodsList = new List<Goods>();
                if (_goodsList != null)
                {
                    _model.ConvertToList((IList)parameter, _goodsList);

                    if (_goodsList.Count == 1)
                    {
                        if (_model.DeleteRecordArhive(_goodsList[0]))
                        {
                            MessageBox.Show("Aрхівну записку виладено", "in", MessageBoxButton.OK);
                            new Thread(new ThreadStart(setFieldDataGrid)).Start();
                        }

                    }
                }
            }
        }
    }
}
