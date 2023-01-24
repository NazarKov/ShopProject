using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class ArchiveViewModel : ViewModel<ArchiveViewModel>
    {
        private ArchiveModel? archiveModel;

        private ICommand searchButton;
        private ICommand visibileAllButton;

        public ArchiveViewModel()
        {
            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            searchButton = new DelegateCommand(SearchArhive);
            visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldGridView)).Start(); });

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
            if (_selectedIndexSearch == 1)
            {   
                Archives = archiveModel.SearchArhive(_nameSearch, TypeSearch.Name);
            }
            else
            {
                Archives = archiveModel.SearchArhive(_nameSearch, TypeSearch.Code);
            }

        }
        public ICommand VisibileAllButton => visibileAllButton;

    }
}
