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
        private ArchiveModel? archiveModel;
        private List<Archive> archives;

        private ICommand searchButton;
        private ICommand visibileAllButton;

        public ArchiveViewModel()
        {
            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            searchButton = new DelegateCommand(SearchArhive);
            visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldGridView)).Start(); });

            archives = new List<Archive>();
            _archives = new List<Archive>();
            _searchTemplateName = new List<string>();
            _nameSearch = string.Empty;

            SetFieldComboBox();
            new Thread(new ThreadStart(SetFieldGridView)).Start();
        }
        private void SetFieldGridView()
        {
            archiveModel = new ArchiveModel();
            Archives = archiveModel.GetItems();
        }

        private void SetFieldComboBox()
        {
            SearchTemplateName = new List<string>();
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        private List<Archive> _archives;
        public List<Archive> Archives
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

        public ICommand SearchButton => searchButton;

        private void SearchArhive()
        {
            switch(_selectedIndexSearch)
            {
                case 0:
                    {
                        Archives = archiveModel.SearchArhive(_nameSearch, TypeSearch.Code);
                        break;
                    }
                case 1:
                    {
                        Archives = archiveModel.SearchArhive(_nameSearch, TypeSearch.Name);
                        break;
                    }
                case 2:
                    {
                        Archives = archiveModel.SearchArhive(_nameSearch, TypeSearch.Articule);
                        break;
                    }

            }
        }

        public ICommand VisibileAllButton => visibileAllButton;

        public ICommand ReturnProductInStorageCommand { get => new DelegateParameterCommand (ReturnProductInStorage,(object parameter)=>true); }
        private void ReturnProductInStorage(object parameter)
        {
            if (MessageBox.Show("Перенести", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                archives = new List<Archive>();
                if (archiveModel != null)
                {
                    archiveModel.ConvertToList((IList)parameter,archives);
                    if(archives.Count == 1)
                    {
                        if(archiveModel.ReturnProductInStorage(archives[0]))
                        {
                            MessageBox.Show("Перенесено товар до складу\nКількість товару буде встановленна на одиницю", "in", MessageBoxButton.OK);
                            new Thread(new ThreadStart(SetFieldGridView)).Start();
                        }
                    }
                }
            }
        }

        public ICommand DeleteArhiveAndProductCommand { get => new DelegateParameterCommand(DeleteArhiveAndProduct, (object parameter) => true); }
        private void DeleteArhiveAndProduct(object parameter)
        {
            if (MessageBox.Show("Ви точно хочете видалити?\nТовар також видаляється.", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                archives = new List<Archive>();
                if (archiveModel != null)
                {
                    archiveModel.ConvertToList((IList)parameter, archives);

                    if (archives.Count == 1)
                    {
                        if (archiveModel.DeleteRecordArhive(archives[0], archives[0].product))
                        {
                            MessageBox.Show("Aрхівну записку виладено","in",MessageBoxButton.OK);
                            new Thread(new ThreadStart(SetFieldGridView)).Start();
                        }

                    }
                }
            }
        }

        
    }
}
