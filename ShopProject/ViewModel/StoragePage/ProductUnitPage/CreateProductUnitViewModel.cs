using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage.ProductUnitPage;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ProductUnitPage
{
    internal class CreateProductUnitViewModel : ViewModel<CreateProductUnitViewModel>
    {
        private readonly CreateProductUnitModel _model;

        private readonly ICommand _createProductUnitCommand;
        private readonly ICommand _exitWindowCommand;

        public CreateProductUnitViewModel()
        {
            _model = new CreateProductUnitModel();


            _fullNameUnit = string.Empty;
            _shortNameUnit = string.Empty;
            _numberUnit = 0;
            _statusUnit = new List<string>();
            _statusUnitEnumType = new List<string>();

            _createProductUnitCommand = new DelegateCommand(CreateProductUnit);
            _exitWindowCommand = new DelegateCommand(() => { });

            SetFieldWindow();
        }

        private void SetFieldWindow()
        {
            SetFieldComboBoxStatusUnit();
        }

        private void SetFieldComboBoxStatusUnit()
        {
            StatusUnit.Add("добавити до обраних");
            StatusUnit.Add("не добавляти до обраних");
             
            _statusUnitEnumType.Add(TypeStatusUnit.Favorite.ToString());
            _statusUnitEnumType.Add(TypeStatusUnit.UnFavorite.ToString());
        }

        private string _fullNameUnit;
        public string FullNameUnit
        {
            get { return _fullNameUnit; }
            set { _fullNameUnit = value; OnPropertyChanged(nameof(FullNameUnit)); }
        }

        private string _shortNameUnit;
        public string ShortNameUnit
        {
            get { return _shortNameUnit; }
            set { _shortNameUnit = value; OnPropertyChanged(nameof(ShortNameUnit)); }
        }

        private int _numberUnit;
        public int NumberUnit
        {
            get { return _numberUnit; }
            set {  _numberUnit = value; OnPropertyChanged(nameof(NumberUnit));}
        }

        private List<string> _statusUnitEnumType;
        private List<string> _statusUnit;
        public List<string> StatusUnit
        {
            get { return _statusUnit; }
            set { _statusUnit = value; OnPropertyChanged(nameof(StatusUnit));}
        }

        private int _selectStatusUnit;
        public int SelectStatusUnit
        {
            get { return _selectStatusUnit;}
            set { _selectStatusUnit = value;OnPropertyChanged(nameof(SelectStatusUnit)); }
        }

        public ICommand CreateProductUnitCommand => _createProductUnitCommand;
        private void CreateProductUnit()
        {
            Task t = Task.Run(async () =>
            {
                await _model.SaveItemDataBase(new ProductUnitEntity()
                {
                    ShortNameUnit = _shortNameUnit,
                    NameUnit = _fullNameUnit,
                    Number = _numberUnit,
                    Status = Enum.Parse<TypeStatusUnit>(_statusUnitEnumType.ElementAt(SelectStatusUnit)),
                });
            });
            t.ContinueWith(t =>
            {
                MessageBox.Show("Одиниця добавленна");
                Mediator.Notify("ReloadUnitsGriedView");
            });
        }

        public ICommand ExitWindowCommand => _exitWindowCommand;

    }
}
