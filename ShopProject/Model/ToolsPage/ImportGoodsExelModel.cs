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
    internal class ImportGoodsExelModel 
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private FileExel? fileExel;
        private List<Goods> _goods;
        private string path;
        private Goods product;

        public ImportGoodsExelModel()
        {
            _goodsRepository = new GoodsTableAccess();
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
            _goods = new List<Goods>();

            var goodslistAll =_goodsRepository.GetAll("in_stock");

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
                        product = new Goods();
                        product.code = Validation.ChekParamsIsNull(code, i, dataTable);
                        product.name = Validation.ChekParamsIsNull(name, i, dataTable);
                        product.articule = Validation.ChekParamsIsNull(articule, i, dataTable);
                        product.price = (decimal)Validation.ChekEmpty(price, i, dataTable);
                        product.count = Convert.ToInt32(Validation.ChekEmpty(count, i, dataTable));

                        if (Validation.ChekParamsIsNull(units, i, dataTable) == "Шт"|| Validation.ChekParamsIsNull(units, i, dataTable) == "шт"|| Validation.ChekParamsIsNull(units, i, dataTable) == "Штука")
                        {
                            product.unit = new GoodsUnit()
                            {
                                number = 2009,
                                name = "шт",
                                shortName = "Штука",
                            };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "пачка"|| Validation.ChekParamsIsNull(units, i, dataTable) == "Пачка"|| Validation.ChekParamsIsNull(units, i, dataTable) == "пач")
                        {
                            product.unit = new GoodsUnit() { number = 2112, name = "пач", shortName = "Пачка" };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "кг" || Validation.ChekParamsIsNull(units, i, dataTable) == "Кілограм")
                        {
                            product.unit = new GoodsUnit() { number = 0301, name = "кг", shortName = "Кілограм" };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "ящ" || Validation.ChekParamsIsNull(units, i, dataTable) == "Ящик")
                        {
                            product.unit = new GoodsUnit() { number = 2075, name = "ящ", shortName = "Ящик" };

                        }

                        product.createdAt = DateTime.Now;
                        product.status = "in_stock";
                        product.codeUKTZED = new CodeUKTZED() { code = "9507" };
                        
                        _goods.Add(product);
                    }
                }
                _goodsRepository.AddRange(_goods);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void SetCountItem(int code,int count, int i , DataTable dataTable)
        {
             Goods product = (Goods)_goodsRepository.GetItemBarCode(Validation.ChekParamsIsNull(code, i, dataTable));
            //_goodsRepository.UpdateParameter(product.id,"count", count);
        }
    }
}
