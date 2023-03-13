using NPOI.SS.Formula.Functions;
using NPOI.Util;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZXing.QrCode.Internal;

namespace ShopProject.Model.ToolsPage
{
    internal class FormationProductModel
    {
        private ITableRepository<Product, TypeParameterSetTableProduct> _tableRepository;

        public FormationProductModel()
        {
            _tableRepository = new ProductTableRepository();
        }

        public Product GetProduct(string barCode)
        {
            try
            {
                return (Product)_tableRepository.GetItem(barCode);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void ContertToListProduct(IList list, List<Product> products)
        {
            foreach (var item in list)
            {
                products.Add((Product)item);
            }
        }

        public List<Product>? UpdateList(List<Product> productFormations, List<Product> removeProduct)
        {
            try
            {
                List<Product> products = new List<Product>();
                products.AddRange(productFormations);
                if (removeProduct.Count == 1)
                {
                    products.Remove(removeProduct[0]);
                    return products;
                }
                else
                {
                    foreach (Product product in removeProduct)
                    {
                        products.Remove(product);
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public bool AddProduct(string name, string code, string articule, double price, int count, string units, List<Product> productsFormation)
        {
            try
            {
                if (Validation.TextField(name,code, articule,price,count,units, (bool)AppSettingsManager.GetParameterFiles("IsValidFormationProduct")))
                {

                    if (Validation.CodeCoincidenceinDatabase(code, (IEnumerable<Product>)_tableRepository.GetAll()))//перевірка на наявність товару по штрих коду
                    {
                        throw new Exception("Товар існує");
                    }
                    Product product = new Product();
                    product.code = code;
                    product.name = name;
                    product.articule = articule;
                    product.price = price;
                    product.count = count;
                    product.units = units;
                    product.created_at = DateTime.Now;
                    product.status = "in_stock";
                    product.sales = 0;


                    _tableRepository.Add(product);
                    foreach (var item in productsFormation)
                    {
                        _tableRepository.SetParameter(item.ID, (item.count - 1), TypeParameterSetTableProduct.Count);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
