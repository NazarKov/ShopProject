using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.FileServise.ExcelServise
{
    public class FileExcelServise
    {
        private readonly DataTableCollection tableCollection;
        //private IWorkbook? workbook;
        public FileExcelServise() { }
        public FileExcelServise(DataTableCollection dataTableCollection)
        {
            tableCollection = dataTableCollection;
        }

        //private void CreateExelTable(List<ExportProductInFileHelper> Product)
        ////{
        ////    if (workbook != null)
        ////    {
        ////       // ISheet sheet1 = workbook.CreateSheet("Продукти");

        ////       // IRow row = sheet1.CreateRow(0);
        ////        CreateExelTableHeader(row);
        ////        int i = 1;
        ////        foreach (var item in Product)
        ////        {
        ////            row = sheet1.CreateRow(i);
        ////            row.CreateCell(0).SetCellValue(item.Product.Code);
        ////            row.CreateCell(1).SetCellValue(item.Product.NameProduct);
        ////            row.CreateCell(2).SetCellValue(item.Product.Articule);
        ////            if (item.Product.Price != null)
        ////                row.CreateCell(3).SetCellValue(item.Product.Price.ToString());
        ////            if (item.Product.Count != null)
        ////                row.CreateCell(4).SetCellValue(item.ProductCount.ToString());
        ////            row.CreateCell(5).SetCellValue(item.Product.Unit.ShortNameUnit);
        ////            i++;
        ////        }

        ////    }
        //}
        ////private void CreateExelTableHeader(IRow row)
        ////{
        ////    row.CreateCell(0).SetCellValue("Шрихкод");
        ////    row.CreateCell(1).SetCellValue("Назва");
        ////    row.CreateCell(2).SetCellValue("Артикуль");
        ////    row.CreateCell(3).SetCellValue("Ціна");
        ////    row.CreateCell(4).SetCellValue("Кількість");
        ////    row.CreateCell(5).SetCellValue("Одиниці");
        ////    row.CreateCell(6).SetCellValue("Знижка");
        ////}

        public DataTable GetTabel(int i)
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
        public List<string> GetHeaders(int i)
        {
            if (tableCollection != null)
            {
                var headers = new List<string>();

                var columns = tableCollection[i].Columns;
                foreach(DataColumn column in columns)
                {
                    headers.Add(column.ColumnName);
                }
                return headers;
            }
            else
            {
                return new List<string>();
            }
        }
        public object GetValue(int colmun, int row , int sheet , bool isSelectRow = false)
        {
            if(tableCollection != null)
            {
                var table = tableCollection[sheet];

                if (isSelectRow)
                {
                    if (table != null)
                    {
                        return table.Rows[colmun][row];
                    }
                }
            }
            return null;
        }
        public int GetMaxRow(int i)
        {
            if (tableCollection != null)
                return tableCollection[i].Rows.Count-1;
            else
                return 0;
        }

    }
}
