using ShopProject.DataBase.Context;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ImportProductExelModel 
    {
        private IEntityGet<ProductEntiti> _productRepositoryGet;
        private IEntityUpdate<ProductEntiti> _productRepositoryUpdate;
        private IEntityAdd<ProductEntiti> _productRepositoryAdd;

        private FileExel? fileExel;
        private List<ProductEntiti> _product;
        private string path;
        private ProductEntiti product;

        public ImportProductExelModel()
        {
            _productRepositoryAdd = new ProductTableAccess();
            _productRepositoryUpdate = new ProductTableAccess();
            _productRepositoryGet = new ProductTableAccess();

            path = string.Empty;
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
        public bool LoadFile()
        {
            try
            {
                fileExel = new FileExel(path);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
        public DataTable? GetTable(int i)
        {
            return fileExel.GetTabel(i);
        }
        public List<string> GetNableName()
        {
            return fileExel.GetTableName();
        }

        public bool SetItemDataBase(DataTable dataTable,params int[] column)
        {
            try
            {
                Validation.ChekRowIsNull(column[0], column[1], column[2], column[3], column[4], column[5]);
                SaveItem(dataTable, column[0], column[1], column[2], column[3], column[4], column[5], column[6], column[7]);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void SaveItem(DataTable dataTable,int code ,int name,int articule,int price,int count,int units,int indexTop,int intdexBottom)
        {
            int i = Validation.ChekNull(indexTop);
            int max = Validation.ChekNull(intdexBottom, dataTable.Rows.Count);
            _product = new List<ProductEntiti>();

            var goodslistAll = new List<ProductEntiti>();
            _productRepositoryGet.GetAll("in_stock");

            try
            {
                for (; i < max; i++)
                {
                    if (Validation.CodeCoincidenceinDatabase(Validation.ChekParamsIsNull(code, i, dataTable),goodslistAll))
                    {
                        SetCountItem(code, Convert.ToInt32(Validation.ChekEmpty(count, i, dataTable)), i, dataTable);
                    }
                    else
                    {
                        product = new ProductEntiti();
                        product.Code = Validation.ChekParamsIsNull(code, i, dataTable);
                        product.NameProduct = Validation.ChekParamsIsNull(name, i, dataTable);
                        product.Articule = Validation.ChekParamsIsNull(articule, i, dataTable);
                        product.Price = (decimal)Validation.ChekEmpty(price, i, dataTable);
                        product.Count = Convert.ToInt32(Validation.ChekEmpty(count, i, dataTable));

                        if (Validation.ChekParamsIsNull(units, i, dataTable) == "Шт"|| Validation.ChekParamsIsNull(units, i, dataTable) == "шт"|| Validation.ChekParamsIsNull(units, i, dataTable) == "Штука")
                        {
                            product.Unit = new ProductUnitEntiti()
                            {
                                Number = 2009,
                                NameUnit = "шт",
                                ShortNameUnit= "Штука",
                            };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "пачка"|| Validation.ChekParamsIsNull(units, i, dataTable) == "Пачка"|| Validation.ChekParamsIsNull(units, i, dataTable) == "пач")
                        {
                            product.Unit = new ProductUnitEntiti() { Number = 2112, NameUnit = "пач", ShortNameUnit = "Пачка" };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "кг" || Validation.ChekParamsIsNull(units, i, dataTable) == "Кілограм")
                        {
                            product.Unit = new ProductUnitEntiti() { Number = 0301, NameUnit = "кг", ShortNameUnit = "Кілограм" };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "ящ" || Validation.ChekParamsIsNull(units, i, dataTable) == "Ящик")
                        {
                            product.Unit = new ProductUnitEntiti() { Number = 2075, NameUnit = "ящ", ShortNameUnit = "Ящик" };

                        }

                        product.CreatedAt = DateTime.Now;
                        product.Status = "in_stock";
                        product.CodeUKTZED = new CodeUKTZEDEntiti() { Code = "9507" };
                        
                        _product.Add(product);
                    }
                }
                _productRepositoryAdd.AddRange(_product);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void SetCountItem(int code,int count, int i , DataTable dataTable)
        {
            ProductEntiti product = (ProductEntiti)_productRepositoryGet.GetByBarCode(Validation.ChekParamsIsNull(code, i, dataTable));
            _productRepositoryUpdate.UpdateParameter(product.ID, nameof(product.Count), count);
        }
    }
}
