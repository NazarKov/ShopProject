using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage.ProductCodeUKTZEDPage;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ProductCodeUKTZEDPage
{
    internal class CreateProductCodeUKTZEDViewModel : ViewModel<CreateProductCodeUKTZEDViewModel>
    {
        private CreateProductCodeUKTZEDModel _model;

        private readonly ICommand _createProductCodeUKTZEDCommand;
        private readonly ICommand _exitWindowCommand;

        public CreateProductCodeUKTZEDViewModel()
        {
            _model = new CreateProductCodeUKTZEDModel();

            _nameCodeUKTZED = string.Empty;
            _code = string.Empty; 
            _statusCodeUKTZED = new List<string>();
            _statusCodeUKTZEDEnumType = new List<string>();

            _createProductCodeUKTZEDCommand = new DelegateCommand(CreateProductCodeUKTZED);
            _exitWindowCommand = new DelegateCommand(() => { });

            SetFieldWindow();
        }

        private void SetFieldWindow()
        {
            SetFieldComboBoxStatusCodeUKTZED();
        }

        private void SetFieldComboBoxStatusCodeUKTZED()
        {
            StatusCodeUKTZED.Add("добавити до обраних");
            StatusCodeUKTZED.Add("не добавляти до обраних");

            _statusCodeUKTZEDEnumType.Add(TypeStatusUnit.Favorite.ToString());
            _statusCodeUKTZEDEnumType.Add(TypeStatusUnit.UnFavorite.ToString());
        }

        private string _nameCodeUKTZED;
        public string NameCodeUKTZED
        {
            get { return _nameCodeUKTZED; }
            set { _nameCodeUKTZED = value; OnPropertyChanged(nameof(NameCodeUKTZED)); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code)); }
        } 

        private List<string> _statusCodeUKTZEDEnumType;
        private List<string> _statusCodeUKTZED;
        public List<string> StatusCodeUKTZED
        {
            get { return _statusCodeUKTZED; }
            set { _statusCodeUKTZED = value; OnPropertyChanged(nameof(StatusCodeUKTZED)); }
        }

        private int _selectStatusCodeUKTZED;
        public int SelectStatusCodeUTKZED
        {
            get { return _selectStatusCodeUKTZED; }
            set { _selectStatusCodeUKTZED = value; OnPropertyChanged(nameof(SelectStatusCodeUTKZED)); }
        }

        public ICommand CreateProductCodeUKTZEDCommand => _createProductCodeUKTZEDCommand;
        private void CreateProductCodeUKTZED()
        {
            Task t = Task.Run(async () =>
            {
                await _model.SaveItemDataBase(new ProductCodeUKTZEDEntity()
                {
                    Code = Code,
                    NameCode = NameCodeUKTZED, 
                    Status = Enum.Parse<TypeStatusCodeUKTZED>(_statusCodeUKTZEDEnumType.ElementAt(SelectStatusCodeUTKZED)),
                });
            });
            t.ContinueWith(t =>
            {
                MessageBox.Show("Одиниця добавленна");
                Mediator.Notify("ReloadCodeUKTEDGriedView");
            });
        }

        public ICommand ExitWindowCommand => _exitWindowCommand;
    }
}
