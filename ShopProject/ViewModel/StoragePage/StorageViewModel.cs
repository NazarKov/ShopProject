using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Views.ToolsPage;
using System;
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
        private ICommand deleteProduct;
        private ICommand openUpdateProductWindow;

        public StorageViewModel()
        {
            Products = new List<Product>();
            SearchTemplateName = new List<string>();
          

            searchButton = new DelegateCommand(SearchProductInCodeAndName);
            visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start(); });
            openCreateProductWindow = new DelegateCommand(CreateProductDatabase);
            openUpdateProductWindow = new DelegateCommand(OpenUpdateProductWindowParameter);
            deleteProduct = new DelegateCommand(DeleteProductDatabase);

            SizeDataGrid = (int)SystemParameters.PrimaryScreenWidth;
            
            _nameSearch = string.Empty;
            _prodcutSelectedProduct = new Product();
            _searchTemplateName = new List<string>();

            SetFieldTextComboBox();

            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
        }

        private void SetFieldTextComboBox()
        {
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
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

        private Product _prodcutSelectedProduct;
        public Product ProdcutSelectedProduct
        {
            get { return _prodcutSelectedProduct; }
            set { _prodcutSelectedProduct = value; OnPropertyChanged("IndexSelectedProduct"); }
        }
        
        public ICommand SearchButton => searchButton;

        void SearchProductInCodeAndName()
        {
            if (storageModel != null)
                if (_selectedIndexSearch == 1)
                {
                    Products = storageModel.SearchProduct(_nameSearch, TypeSearch.Name);
                }
                else
                {
                    Products = storageModel.SearchProduct(_nameSearch, TypeSearch.Code);
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

        public ICommand OpenUpdateProductWindow => openUpdateProductWindow;
      
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
            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
        }

        public ICommand DeleteProduct => deleteProduct;

        private void DeleteProductDatabase()
        {
            if (_prodcutSelectedProduct != null)
                if (storageModel != null)
                {
                    if(storageModel.DeleteProduct(_prodcutSelectedProduct))
                        new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
                }
        }
    }
}
