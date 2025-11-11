 
using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage.ProductUnitPage;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ProductUnitPage
{
    internal class UpdateProductUnitViewModel : ViewModel<UpdateProductUnitViewModel>
    {
        private UpdateProductUnitModel _model;

        private readonly ICommand _updateProductUnitCommand;
        private readonly ICommand _exitWindowCommand;

        private ProductUnit _unit;

        public UpdateProductUnitViewModel()
        {
            _model = new UpdateProductUnitModel(); 


            _fullNameUnit = string.Empty;
            _shortNameUnit = string.Empty;
            _numberUnit = 0;
            _statusUnit = new List<string>();
            _statusUnitEnumType = new List<string>();
            _unit = new ProductUnit();

            _updateProductUnitCommand = new DelegateCommand(UpdateProductUnit);
            _exitWindowCommand = new DelegateCommand(() => { });

            SetFieldWindow();
        }

        private void SetFieldWindow()
        {
            _unit = Session.ProductUnit;

            SetFieldComboBoxStatusUnit();
            if (_unit != null) 
            {
                FullNameUnit = _unit.NameUnit;
                ShortNameUnit = _unit.ShortNameUnit;
                NumberUnit = _unit.Number;

                for (int i = 0; i < StatusUnit.Count; i++)
                {
                    if (_statusUnitEnumType.ElementAt(i) == _unit.Status.ToString())
                    {
                        SelectStatusUnit = i;
                    }
                }
            }

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
            set { _numberUnit = value; OnPropertyChanged(nameof(NumberUnit)); }
        }

        private List<string> _statusUnitEnumType;
        private List<string> _statusUnit;
        public List<string> StatusUnit
        {
            get { return _statusUnit; }
            set { _statusUnit = value; OnPropertyChanged(nameof(StatusUnit)); }
        }

        private int _selectStatusUnit;
        public int SelectStatusUnit
        {
            get { return _selectStatusUnit; }
            set { _selectStatusUnit = value; OnPropertyChanged(nameof(SelectStatusUnit)); }
        }

        public ICommand UpdateProductUnitCommand => _updateProductUnitCommand;
        private void UpdateProductUnit()
        {
            Task t = Task.Run(async () =>
            {
                await _model.UpdateItemDataBase(new ProductUnit()
                {
                    ID = _unit.ID,
                    ShortNameUnit = _shortNameUnit,
                    NameUnit = _fullNameUnit,
                    Number = _numberUnit,
                    Status = Enum.Parse<TypeStatusUnit>(_statusUnitEnumType.ElementAt(SelectStatusUnit)),
                });
            });
            t.ContinueWith(t =>
            {
                MessageBox.Show("Одиниця оновленна");
                MediatorService.ExecuteEvent("ReloadUnitsGriedView");
            });
        }

        public ICommand ExitWindowCommand => _exitWindowCommand;
    }
}
