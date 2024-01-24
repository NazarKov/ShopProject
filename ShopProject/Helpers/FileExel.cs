using ExcelDataReader;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace ShopProject.Helpers
{
    internal class FileExel
    {
        private DataTableCollection? tableCollection = null;
        private IWorkbook? workbook;

        public FileExel() { }

        public FileExel(string filePath)
        {
            Read(filePath);
        }

        private void Read(string path)
        {
            try
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader read = ExcelReaderFactory.CreateReader(stream);

                    DataSet db = read.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    tableCollection = db.Tables;
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Write(string path, List<HelperClassExportGoodsInFile> Goods)
        {
            workbook = new XSSFWorkbook();
            CreateExelTable(Goods);

            FileStream sw = File.Create(path);
            workbook.Write(sw, true);
            sw.Close();

        }
        private void CreateExelTable(List<HelperClassExportGoodsInFile> Goods)
        {
            if (workbook != null)
            {
                ISheet sheet1 = workbook.CreateSheet("Продукти");

                IRow row = sheet1.CreateRow(0);
                CreateExelTableHeader(row);
                int i = 1;
                foreach (var item in Goods)
                {
                    row = sheet1.CreateRow(i);
                    row.CreateCell(0).SetCellValue(item.goods.code);
                    row.CreateCell(1).SetCellValue(item.goods.name);
                    row.CreateCell(2).SetCellValue(item.goods.articule);
                    if (item.goods.price != null)
                        row.CreateCell(3).SetCellValue(item.goods.price.ToString());
                    if (item.goods.count != null)
                        row.CreateCell(4).SetCellValue(item.goodsCount.ToString());
                    row.CreateCell(5).SetCellValue(item.goods.unit.shortName);
                    i++;
                }

            }
        }
        private void CreateExelTableHeader(IRow row)
        {
            row.CreateCell(0).SetCellValue("Шрихкод");
            row.CreateCell(1).SetCellValue("Назва");
            row.CreateCell(2).SetCellValue("Артикуль");
            row.CreateCell(3).SetCellValue("Ціна");
            row.CreateCell(4).SetCellValue("Кількість");
            row.CreateCell(5).SetCellValue("Одиниці");
            row.CreateCell(6).SetCellValue("Знижка");
        }

        public DataTable? GetTabel(int i)
        {
            if (tableCollection != null)
                return tableCollection[i];
            else
                return null;
        }

        public List<string> GetTableName()
        {
            List<string> tableName = new List<string>();
            if (tableCollection != null)
                foreach (DataTable table in tableCollection)
                {
                    tableName.Add(table.TableName);
                }
            return tableName;
        }
    }
}
