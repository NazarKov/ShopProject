using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.HomePage;
using ShopProject.Views.ToolsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.HomePage
{
    internal class StorageViewModel : ViewModel<StorageViewModel>
    {
        StorageModel storageModel;
        private ICommand searchButton;
        private ICommand visibileAllButton;
        private ICommand openCreateProductWindow;
        private ICommand deleteProduct;
        private ICommand openUpdateProductWindow;

        public StorageViewModel()
        {
            storageModel = new StorageModel();
            Products = new List<Product>();
            SearchTemplateName = new List<string>();
         
            searchButton = new DelegateCommand(Search);
            visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(VisibileAllProductThread)).Start(); });
            openCreateProductWindow = new DelegateCommand(() => { new CreateProductPage().ShowDialog(); });
            openUpdateProductWindow = new DelegateCommand(OpenUpdateProductWindowParameter);

            deleteProduct = new DelegateCommand(() => { storageModel.DeleteProduct(_prodcutSelectedProduct);});

            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SelectedIndexSearch = 0;

            new Thread(new ThreadStart(addItemThread)).Start();

        }

        void addItemThread()
        {
            Products = storageModel.GetItemsLoadDb(); 
        }

        private List<Product> _product;
        public List<Product> Products
        {
            set
            {
                _product = value;
                OnPropertyChanged("Products");
            }
            get { return _product; }
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
            get { return _searchTemplateName;}
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
            get { return nameSearch;}
        }

        public ICommand SearchButton => searchButton;

        void Search()
        {
            if (_selectedIndexSearch == 1)
            {
                Products = storageModel.Search(nameSearch, StorageModel.TypeSearch.Name);
            }
            else
            {
                Products = storageModel.Search(nameSearch,StorageModel.TypeSearch.Code);
            }
    
        }
        public ICommand VisibileAllButton => visibileAllButton;

        void VisibileAllProductThread()
        {
            Products = null;
            Products = storageModel.GetItems();
        }

        public ICommand OpenCreateProductWindow => openCreateProductWindow;
        public ICommand OpenUpdateProductWindow => openUpdateProductWindow;


        private Product _prodcutSelectedProduct;
        public Product ProdcutSelectedProduct
        {
            get { return _prodcutSelectedProduct; }
            set { _prodcutSelectedProduct = value; OnPropertyChanged("IndexSelectedProduct"); }
        }

        public ICommand DeleteProduct => deleteProduct;
       
        void OpenUpdateProductWindowParameter()
        {
            if (_prodcutSelectedProduct != null)
            {
                ResourseProductModel.product = _prodcutSelectedProduct;
                new UpdateProduct().ShowDialog();
            }
            else
            {
                MessageBox.Show("Продук не вибраний для редагування");
            }
            new Thread(new ThreadStart(addItemThread)).Start();
        }
    }
}
