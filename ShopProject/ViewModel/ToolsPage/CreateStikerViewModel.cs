using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FontFamily = System.Windows.Media.FontFamily;
using Image = System.Windows.Controls.Image;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class CreateStikerViewModel : ViewModel<CreateStikerViewModel>
    {
        private ICommand createStikerCommand;
        private ICommand printStikerCommand;
        private ICommand clearStikerCommand;
        private CreateStikerModel model;

        public CreateStikerViewModel()
        {
            model = new CreateStikerModel();

            createStikerCommand = new DelegateCommand(CreateStiker);
            printStikerCommand = new DelegateCommand(Print);
            clearStikerCommand = new DelegateCommand(ClearField);

            _nameProduct = string.Empty;
            _nameCompany = string.Empty;
            _code = string.Empty;
            _description = string.Empty;
            _barCode = new BitmapImage();
        }

        private string _nameProduct;
        public string NameProduct
        {
            get { return _nameProduct; }
            set { _nameProduct = value; OnPropertyChanged("NameProduct"); }
        }

        private string _nameCompany;
        public string NameCompany
        {
            get { return _nameCompany; }
            set { _nameCompany = value; OnPropertyChanged("NameCompany"); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged("Code"); }
        }
        
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        private BitmapImage _barCode;
        public BitmapImage BarCode
        {
            get { return _barCode; }
            set { _barCode = value; OnPropertyChanged("BarCode"); }
        }

        public ICommand CreateStikerComman => createStikerCommand;

        private void CreateStiker()
        {
            BarCode = model.CreateBarCode(_nameCompany, _nameProduct, _description, _code);
        }

        public ICommand PrintStikerCommand => printStikerCommand;

        private void Print()
        {
            Image image = new Image();
            image.Source = BarCode;
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(image, "file");
            }
        }

        public ICommand ClearStikerCommand => clearStikerCommand;

        private void ClearField()
        {
            Code = string.Empty;
            NameCompany = string.Empty;
            NameProduct = string.Empty;
            Description = string.Empty;
            BarCode = model.Clear();
        }

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;


    }
}
