using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingGeneralViewModel : ViewModel<SettingGeneralViewModel>
    {
        private ICommand _updateFieldIsValidCreateProductCommand;
        private ICommand _updateFieldIsValidUpdateProductCommand;

        private SettingGeneralModel _model;

        public SettingGeneralViewModel() 
        {
            _updateFieldIsValidCreateProductCommand = new DelegateCommand(UpdateFieldCreateProduct);
            _updateFieldIsValidUpdateProductCommand = new DelegateCommand(UpdateFieldUpdateProduct);

             _model = new SettingGeneralModel();

            SetFieldCheckBox();
        }
        private void SetFieldCheckBox()
        {
            IsValidCrateProduct = (bool)AppSettingsManager.GetParameterFiles("IsValidCreateProduct");
            IsValidUpdateProduct = (bool)AppSettingsManager.GetParameterFiles("IsValidUpdateProduct");
        }

        private bool _isValidCrateProduct;
        public bool IsValidCrateProduct
        {
            get { return _isValidCrateProduct; }
            set { _isValidCrateProduct = value; OnPropertyChanged("IsValidCrateProduct"); }
        }

        private bool _isValidUpdateProduct;
        public bool IsValidUpdateProduct
        {
            get { return _isValidUpdateProduct; }
            set { _isValidUpdateProduct = value; OnPropertyChanged("IsValidUpdateProduct"); }
        }

        public ICommand UpdateFieldIsValidCreateProductCommand => _updateFieldIsValidCreateProductCommand;

        private void UpdateFieldCreateProduct()
        {
            _model.updateField(IsValidCrateProduct, TypeUpdateFieldIsValid.CreateProduct);
        }

        public ICommand UpdateFieldIsValidUpdateProductCommand => _updateFieldIsValidUpdateProductCommand;

        private void UpdateFieldUpdateProduct()
        {
            _model.updateField(IsValidUpdateProduct, TypeUpdateFieldIsValid.UpdateProduct);
        }
    }
}
