using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace ShopProject.Model.ToolsPage
{
    internal class CreateProductModel
    {
        private ITableRepository<Product, TypeParameterSetTableProduct> _productRepository;

        public CreateProductModel()
        {
            _productRepository = new ProductTableRepository();
        }

        public bool SaveItemDataBase(string name, string code, string articule, double price, int count, string units)
        {
            try
            {
                if (Validation.TextField(name, code, articule, price, count,units, (bool)AppSettingsManager.GetParameterFiles("IsValidCreateProduct")))
                {
                    if (Validation.CodeCoincidenceinDatabase(code, (IEnumerable<Product>)_productRepository.GetAll()))//перевірка на наявність товару по штрих коду
                    {
                        throw new Exception("Товар існує");
                    }
                    _productRepository.Add(new Product()
                    {
                        name = name,
                        code = code,
                        articule = articule,
                        price = price,
                        count = count,
                        units = units,
                        created_at = DateTime.Now,
                        status = "in_stock",
                        sales = 0
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
