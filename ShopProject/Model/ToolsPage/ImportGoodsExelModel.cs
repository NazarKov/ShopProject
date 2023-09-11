﻿using ShopProject.DataBase.Context;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
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
         IEntityAccessor<Goods> _goodsRepository;
        private FileExel? fileExel;
        private List<Goods> products;
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
            products = new List<Goods>();

            try
            {
                for (; i < max; i++)
                {
                    if (Validation.CodeCoincidenceinDatabase(Validation.ChekParamsIsNull(code, i, dataTable),_goodsRepository.GetAll("in_stock")))
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
                        if (Validation.ChekParamsIsNull(units, i, dataTable) == "Шт"|| Validation.ChekParamsIsNull(units, i, dataTable) == "шт")
                        {
                            product.unit = new GoodsUnit()
                            {
                                number = 2009,
                                name = "шт",
                                shortName = "Штука",
                            };
                        }
                        else if (Validation.ChekParamsIsNull(units, i, dataTable) == "пачка")
                        {
                            product.unit = new GoodsUnit() { number = 2112, name = "пач", shortName = "Пачка" };
                        }
                        product.createdAt = DateTime.Now;
                        product.status = "in_stock";
                        product.codeUKTZED = new CodeUKTZED() { code = "9507" };
                        products.Add(product);
                    }
                }
                _goodsRepository.AddRange(products);
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