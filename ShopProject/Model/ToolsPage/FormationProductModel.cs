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
        private ITableRepository<Goods, TypeParameterSetTableProduct> _tableRepository;

        public FormationProductModel()
        {
            _tableRepository = new ProductTableRepository();
        }

        public Goods GetProduct(string barCode)
        {
            try
            {
                return (Goods)_tableRepository.GetItem(barCode);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void ContertToListProduct(IList list, List<Goods> products)
        {
            foreach (var item in list)
            {
                products.Add((Goods)item);
            }
        }

        public List<Goods>? UpdateList(List<Goods> productFormations, List<Goods> removeProduct)
        {
            try
            {
                List<Goods> products = new List<Goods>();
                products.AddRange(productFormations);
                if (removeProduct.Count == 1)
                {
                    products.Remove(removeProduct[0]);
                    return products;
                }
                else
                {
                    foreach (Goods product in removeProduct)
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
        public bool AddProduct(string name, string code, string articule, double price, int count, string units, List<Goods> productsFormation)
        {
            try
            {
                if (Validation.TextField(name,code, articule,price,count,units, (bool)AppSettingsManager.GetParameterFiles("IsValidFormationProduct")))
                {

                    if (Validation.CodeCoincidenceinDatabase(code, (IEnumerable<Goods>)_tableRepository.GetAll()))//перевірка на наявність товару по штрих коду
                    {
                        throw new Exception("Товар існує");
                    }
                    Goods product = new Goods();
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
