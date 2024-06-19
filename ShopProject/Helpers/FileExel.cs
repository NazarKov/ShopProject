using ExcelDataReader;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ShopProject.DataBase.Model;
using ShopProject.Helpers.DataGridViewHelperModel;
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

        public void Write(string path, List<ExportProductInFileHelper> Product)
        {
            workbook = new XSSFWorkbook();
            CreateExelTable(Product);

            FileStream sw = File.Create(path);
            workbook.Write(sw, true);
            sw.Close();

        }
        private void CreateExelTable(List<ExportProductInFileHelper> Product)
        {
            if (workbook != null)
            {
                ISheet sheet1 = workbook.CreateSheet("Продукти");

                IRow row = sheet1.CreateRow(0);
                CreateExelTableHeader(row);
                int i = 1;
                foreach (var item in Product)
                {
                    row = sheet1.CreateRow(i);
                    row.CreateCell(0).SetCellValue(item.Product.Code);
                    row.CreateCell(1).SetCellValue(item.Product.NameProduct);
                    row.CreateCell(2).SetCellValue(item.Product.Articule);
                    if (item.Product.Price != null)
                        row.CreateCell(3).SetCellValue(item.Product.Price.ToString());
                    if (item.Product.Count != null)
                        row.CreateCell(4).SetCellValue(item.ProductCount.ToString());
                    row.CreateCell(5).SetCellValue(item.Product.Unit.ShortNameUnit);
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
