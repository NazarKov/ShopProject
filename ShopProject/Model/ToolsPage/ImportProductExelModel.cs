using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Data; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ImportProductExelModel 
    {

        private FileExel? fileExel;
        private List<ProductEntity> _product;
        private string path;
        private ProductEntity product;

        public ImportProductExelModel()
        {  
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

        public bool SetItemDataBase(DataTable dataTable, params int[] column)
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

        private void SaveItem(DataTable dataTable, int code, int name, int articule, int price, int count, int units, int indexTop, int intdexBottom)
        {
            int i = Validation.ChekNull(indexTop);
            int max = Validation.ChekNull(intdexBottom, dataTable.Rows.Count);
            _product = new List<ProductEntity>();
             

            List<ProductEntity> goodslistAll = new List<ProductEntity>();

            Task task = Task.Run(async () => {

                goodslistAll = (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProducts(Session.Token)).ToList();
            });

            task.Wait();


            try
            {
                for (; i < max; i++)
                {
                    if (Validation.CodeCoincidenceinDatabase(Validation.ChekParamsIsNull(code, i, dataTable), goodslistAll))
                    {
                        SetCountItem(code, Convert.ToInt32(Validation.ChekEmpty(count, i, dataTable)), i, dataTable);
                    }
                    else
                    {
                        product = new ProductEntity();
                        product.Code = Validation.ChekParamsIsNull(code, i, dataTable);
                        product.NameProduct = Validation.ChekParamsIsNull(name, i, dataTable);
                        product.Articule = Validation.ChekParamsIsNull(articule, i, dataTable);
                        product.Price = (decimal)Validation.ChekEmpty(price, i, dataTable);
                        product.Count = Convert.ToInt32(Validation.ChekEmpty(count, i, dataTable));

                        if (Validation.ChekParamsIsNull(units, i, dataTable) == "Шт" || Validation.ChekParamsIsNull(units, i, dataTable) == "шт" || Validation.ChekParamsIsNull(units, i, dataTable) == "Штука")
                        {
                            product.Unit = new ProductUnitEntity()
                            {
                                Number = 2009,
                                NameUnit = "шт",
                                ShortNameUnit = "Штука",
                            };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "пачка" || Validation.ChekParamsIsNull(units, i, dataTable) == "Пачка" || Validation.ChekParamsIsNull(units, i, dataTable) == "пач")
                        {
                            product.Unit = new ProductUnitEntity() { Number = 2112, NameUnit = "пач", ShortNameUnit = "Пачка" };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "кг" || Validation.ChekParamsIsNull(units, i, dataTable) == "Кілограм")
                        {
                            product.Unit = new ProductUnitEntity() { Number = 0301, NameUnit = "кг", ShortNameUnit = "Кілограм" };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "ящ" || Validation.ChekParamsIsNull(units, i, dataTable) == "Ящик")
                        { 
                            product.Unit = new ProductUnitEntity() { Number = 2075, NameUnit = "ящ", ShortNameUnit = "Ящик" };

                        }

                        product.CreatedAt = DateTime.Now;
                        product.Status =  TypeStatusProduct.InStock;
                        product.CodeUKTZED = new ProductCodeUKTZEDEntity() { Code = "9507" };

                        _product.Add(product);
                    }
                }
                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await MainWebServerController.MainDataBaseConntroller.ProductController.AddProductRange(Session.Token,_product);
                });
                t.Wait();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void SetCountItem(int code, int count, int i, DataTable dataTable)
        {
            Task t = Task.Run(async () =>
            {
                //await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateParameterProduct(Session.Token, nameof(ProductEntity.Count), count, new ProductEntity() { Code = Validation.ChekParamsIsNull(code, i, dataTable) });
            });
        }
    }
}
