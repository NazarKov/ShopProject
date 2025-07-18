using NPOI.Util;
using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage.ProductCodeUKTZEDPage;
using ShopProjectDataBase.DataBase.Model;
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
    internal class UpdateProductCodeUKTZEDViewModel : ViewModel<UpdateProductCodeUKTZEDViewModel>
    {
        private UpdateProductCodeUKTZEDModel _model;

        private readonly ICommand _updateProductCodeUKTZEDCommand;
        private readonly ICommand _exitWindowCommand;

        private ProductCodeUKTZEDEntity _codeUKTZED;

        public UpdateProductCodeUKTZEDViewModel()
        {
            _model = new UpdateProductCodeUKTZEDModel();
             

            _nameCodeUKTZED = string.Empty;
            _code = string.Empty;
            _statusCodeUKTZED = new List<string>();
            _statusCodeUKTZEDEnumType = new List<string>();
            _codeUKTZED = new ProductCodeUKTZEDEntity();

            _updateProductCodeUKTZEDCommand = new DelegateCommand(UpdateProductCodeUKTZED);
            _exitWindowCommand = new DelegateCommand(() => { });

            SetFieldWindow();
        }

        private void SetFieldWindow()
        {

            _codeUKTZED = Session.ProductCodeUKTZEDEntity;

            SetFieldComboBoxStatusCodeUKTZED();

            if (_codeUKTZED != null)
            {
                NameCodeUKTZED = _codeUKTZED.NameCode;
                Code = _codeUKTZED.Code; 

                for (int i = 0; i < StatusCodeUKTZED.Count; i++)
                {
                    if (_statusCodeUKTZEDEnumType.ElementAt(i) == _codeUKTZED.Status.ToString())
                    {
                        SelectStatusCodeUTKZED = i;
                    }
                }
            }

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

        public ICommand UpdateProductCodeUKTZEDCommand => _updateProductCodeUKTZEDCommand;
        private void UpdateProductCodeUKTZED()
        {
            Task t = Task.Run(async () =>
            {
                await _model.UpdateItemDataBase(new ShopProjectDataBase.DataBase.Model.ProductCodeUKTZEDEntity()
                {
                    ID = _codeUKTZED.ID,
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
