using NPOI.SS.Formula.Atp;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Views.ToolsPage;
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
    internal class StorageViewModel : ViewModel<StorageViewModel>
    {
        StorageModel? storageModel;

        private ICommand searchButton;
        private ICommand visibileAllButton;
        private ICommand openCreateProductWindow;
        private List<Product> products;
        
        public StorageViewModel()
        {
            Products = new List<Product>();
            SearchTemplateName = new List<string>();
          

            searchButton = new DelegateCommand(SearchProductInCodeAndName);
            visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start(); });
            openCreateProductWindow = new DelegateCommand(CreateProductDatabase);
       
            SizeDataGrid = (int)SystemParameters.PrimaryScreenWidth;
            products = new List<Product>();

            _nameSearch = string.Empty;
            _searchTemplateName = new List<string>();

            SetFieldTextComboBox();

            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
        }

        private void SetFieldTextComboBox()
        {
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        private List<Product>? _product;
        public List<Product>? Products
        {
            get { return _product; }
            set{ _product = value; OnPropertyChanged("Products"); }
        }

        private int _sizeDataGrid;
        public int SizeDataGrid
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

        public ICommand SearchButton => searchButton;

        void SearchProductInCodeAndName()
        {
            if (storageModel != null)
                switch (_selectedIndexSearch)
                {
                    case 0:
                        {
                            Products = storageModel.SearchProduct(_nameSearch, TypeSearch.Code);
                            break;
                        }
                    case 1:
                        {
                            Products = storageModel.SearchProduct(_nameSearch, TypeSearch.Name);
                            break;
                        }
                    case 2:
                        {
                            Products = storageModel.SearchProduct(_nameSearch, TypeSearch.Articule);
                            break;
                        }
                }
        }
        public ICommand VisibileAllButton => visibileAllButton;

        void SetFieldItemDataGridThread()
        {
            Products = null;
            storageModel = new StorageModel();
            Products = storageModel.GetItems();
        }

        public ICommand OpenCreateProductWindow => openCreateProductWindow;

        private void CreateProductDatabase()
        {
            if (storageModel != null)
            {
                if(new CreateProductPage().ShowDialog().HasValue)
                    new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
            }
        }

        public ICommand UpdateProductCommand { get => new DelegateParameterCommand(EditingProduct, CanRegister); }
        private void EditingProduct(object parameter)
        {
            products = new List<Product>();
            if(storageModel!=null)
            storageModel.ContertToListProduct((IList)parameter, products);

            if(products.Count==1)
            {
                StaticResourse.product = products[0];
                new UpdateProduct().ShowDialog();
            }
            else
            {
                StaticResourse.products = products; 
                new UpdateProductAll().ShowDialog();
            }

        }

        public ICommand DeleteProductCommand { get => new DelegateParameterCommand(DeleteProduct, CanRegister); }
        private void DeleteProduct(object parameter)
        {
            if (MessageBox.Show("видалити?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                products = new List<Product>();
                if (storageModel != null)
                    storageModel.ContertToListProduct((IList)parameter, products);
                if (products.Count == 1)
                {
                    if (products[0] != null)
                        if (storageModel != null)
                        {
                            if (storageModel.DeleteProduct(products[0]))
                                new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
                        }
                }
            }
        }

        public ICommand AddProductArhiveCommand { get => new DelegateParameterCommand(AddProductArhive, CanRegister); }
        private void AddProductArhive(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                products = new List<Product>();
                if (storageModel != null)
                {
                    storageModel.ContertToListProduct((IList)parameter, products);
                    if (products.Count == 1)
                    {

                        storageModel.SetProductInArhive(products[0]);
                    }
                }
            }
        }
        private bool CanRegister(object parameter) => true;


    }
}
