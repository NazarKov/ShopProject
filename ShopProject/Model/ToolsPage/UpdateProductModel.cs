using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductModel
    {
        //private IEntityAccessor<Goods, TypeParameterSetTableProduct> _productRepository;

        public UpdateProductModel()
        {
          //  _productRepository = new ProductTableRepository();
        }
      
        public bool UpdateProduct(int id,string name, string code,string articule, double price, int count, string units)
        {
            try
            {
                if(Validation.TextField(name, code, articule, price, count, units, (bool)AppSettingsManager.GetParameterFiles("IsValidUpdateProduct")))
                {
                    //_productRepository.Update(new Goods()
                    //{
                    //    id = id,
                    //    name=name,
                    //    code=code,
                    //    articule=articule,
                    //    price=(decimal)price,
                    //    count=count,
                    //    units=units
                    //});
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

      
    }
}
