using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace ShopProject.ViewModel.StoragePage
{
    internal class ArchiveViewModel : ViewModel<ArchiveViewModel>
    {
        private ArchiveModel? _model;
        private List<Goods> _goodsList;

        private ICommand _searchCommand;
        private ICommand _visibileAllCommand;

        public ArchiveViewModel()
        {
            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            _searchCommand = new DelegateCommand(SearchArhive);
            _visibileAllCommand = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldGridView)).Start(); });

            _goodsList = new List<Goods>();
            _archives = new List<Goods>();
            _searchTemplateName = new List<string>();
            _nameSearch = string.Empty;

            SetFieldComboBox();
            new Thread(new ThreadStart(SetFieldGridView)).Start();
        }
        private void SetFieldGridView()
        {
            _model = new ArchiveModel();
            Archives = _model.GetItems();
        }

        private void SetFieldComboBox()
        {
            SearchTemplateName = new List<string>();
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        private List<Goods> _archives;
        public List<Goods> Archives
        {
            get { return _archives; }
            set { _archives = value; OnPropertyChanged("Archives"); }
        }

        private int _sizeDataGrid;
        public int SizeDataGrid
        {
            set { _sizeDataGrid = value; OnPropertyChanged("SizeDataGrid"); }
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

        private void SearchArhive()
        {
            switch(_selectedIndexSearch)
            {
                case 0:
                    {
                        Archives = _model.SearchArhive(_nameSearch, TypeSearch.Code);
                        break;
                    }
                case 1:
                    {
                        Archives = _model.SearchArhive(_nameSearch, TypeSearch.Name);
                        break;
                    }
                case 2:
                    {
                        Archives = _model.SearchArhive(_nameSearch, TypeSearch.Articule);
                        break;
                    }

            }
        }

        public ICommand VisibileAllCommand => _visibileAllCommand;

        public ICommand ReturnGoodsInStorageCommand { get => new DelegateParameterCommand (ReturnGoodsInStorage,(object parameter)=>true); }
        private void ReturnGoodsInStorage(object parameter)
        {   
            if (MessageBox.Show("Перенести", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goodsList = new List<Goods>();
                if (_model != null)
                {
                    _model.ConvertToList((IList)parameter, _goodsList);
                    if (_goodsList.Count == 1)
                    {
                        if (_model.ReturnGoodsInStorage(_goodsList[0]))
                        {
                            new Thread(new ThreadStart(SetFieldGridView)).Start();
                        }
                    }
                }
            }
        }

        public ICommand DeleteArhiveAndGoodsCommand { get => new DelegateParameterCommand(DeleteArhiveAndGoods, (object parameter) => true); }
        private void DeleteArhiveAndGoods(object parameter)
        {
            if (MessageBox.Show("Ви точно хочете видалити?\nТовар також видаляється.", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goodsList = new List<Goods>();
                if (_model != null)
                {
                    _model.ConvertToList((IList)parameter, _goodsList);

                    if (_goodsList.Count == 1)
                    {
                        if (_model.DeleteRecordArhive(_goodsList[0]))
                        {
                            MessageBox.Show("Aрхівну записку виладено", "in", MessageBoxButton.OK);
                            new Thread(new ThreadStart(SetFieldGridView)).Start();
                        }

                    }
                }
            }
        }

        
    }
}
