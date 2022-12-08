using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.HomePage
{
    internal class ArchiveViewModel : ViewModel<ArchiveViewModel>
    {
        ArchiveModel archiveModel;

        private ICommand searchButton;
        private ICommand visibileAllButton;

        public ArchiveViewModel()
        {
            archiveModel = new ArchiveModel();
            SearchTemplateName = new List<string>();
            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            searchButton = new DelegateCommand(Search);
            visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(VisibileAllProductThread)).Start(); });

            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SelectedIndexSearch = 0;

            new Thread(new ThreadStart(addItem)).Start();
        }
        void addItem()
        {
           Archives=archiveModel.GetItemsLoadDb();
        }

        private List<Archive> _archives;
        public List<Archive> Archives
        {
            set { 
                _archives = value;
                OnPropertyChanged("Archives");
            }
            get { return _archives; }
        }

        private int _sizeDataGrid;
        public int SizeDataGrid
        {
            set
            {
                _sizeDataGrid = value;
                OnPropertyChanged("SizeDataGrid");
            }
        }

        private List<string> _searchTemplateName;
        public List<string> SearchTemplateName
        {
            set
            {
                _searchTemplateName = value;
                OnPropertyChanged("SearchTemplateName");
            }
            get { return _searchTemplateName; }
        }

        private int _selectedIndexSearch;
        public int SelectedIndexSearch
        {
            set
            {
                _selectedIndexSearch = value;
                OnPropertyChanged("SelectedIndexSearch");
            }
            get { return _selectedIndexSearch; }
        }

        private string nameSearch;
        public string NameSearch
        {
            set
            {
                nameSearch = value;
                OnPropertyChanged("NameSearch");
            }
            get { return nameSearch; }
        }

        public ICommand SearchButton => searchButton;

        void Search()
        {
            if (_selectedIndexSearch == 1)
            {
                Archives = archiveModel.Search(nameSearch,ArchiveModel.TypeSearch.Name);
            }
            else
            {
                Archives = archiveModel.Search(nameSearch,ArchiveModel.TypeSearch.Code);
            }

        }
        public ICommand VisibileAllButton => visibileAllButton;

        void VisibileAllProductThread()
        {
            Archives = archiveModel.GetItems();
        }

    }
}
