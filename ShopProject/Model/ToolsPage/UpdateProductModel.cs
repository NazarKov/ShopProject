﻿using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductModel
    {
        private ITableRepository<Product, TypeParameterSetTableProduct> _productRepository;

        public UpdateProductModel()
        {
            _productRepository = new ProductTableRepository();
        }
      
        public bool UpdateProduct(int id,string name, string code,string articule, double price, int count, string units)
        {
            try
            {
                if(Validation.TextField(name, code, articule, price, count, units, false))
                {
                    _productRepository.Update(new Product()
                    {
                        ID = id,
                        name=name,
                        code=code,
                        articule=articule,
                        price=price,
                        count=count,
                        units=units
                    });
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
