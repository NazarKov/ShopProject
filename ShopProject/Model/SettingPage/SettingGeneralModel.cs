using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SettingPage
{
    public enum TypeUpdateFieldIsValid
    {
        CreateProduct = 1,
        UpdateProduct = 2,
    }

    internal class SettingGeneralModel
    {
        public SettingGeneralModel() { }

        public void updateField(bool isFiled, TypeUpdateFieldIsValid type)
        {
            try
            {
                switch(type)
                {
                    case TypeUpdateFieldIsValid.CreateProduct:
                        {
                            if (isFiled)
                            {
                                AppSettingsManager.SetParameterFile("IsValidCreateProduct", true);
                            }
                            else
                            {
                                AppSettingsManager.SetParameterFile("IsValidCreateProduct", false);
                            }
                            break;
                        }
                    case TypeUpdateFieldIsValid.UpdateProduct:
                        {
                            if(isFiled)
                            {
                                AppSettingsManager.SetParameterFile("IsValidUpdateProduct", true);
                            }
                            else
                            {
                                AppSettingsManager.SetParameterFile("IsValidUpdateProduct", false);
                            }
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
