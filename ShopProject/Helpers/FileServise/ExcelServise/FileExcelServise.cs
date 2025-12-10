using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ShopProject.UIModel.StoragePage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.FileServise.ExcelServise
{
    public class FileExcelServise
    {
        private readonly DataTableCollection tableCollection;
        private XLWorkbook? workbook;
        public FileExcelServise()
        {
            workbook = new XLWorkbook(); 
        }
        public FileExcelServise(DataTableCollection dataTableCollection)
        {
            tableCollection = dataTableCollection;
            workbook = new XLWorkbook();
        }

        public XLWorkbook CreateExelTable(DataTable dataTable)
        {
            if (workbook != null)
            {
                var ws = workbook.Worksheets.Add(dataTable.TableName);
                 
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    ws.Cell(1, col + 1).Value = dataTable.Columns[col].ColumnName;
                }
                 
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {

                        var item = dataTable.Rows[row][col]; 
                        ws.Cell(row + 2, col + 1).Value = GetValue(item);
                    }
                }
                return workbook;
            }
            throw new Exception();
        }

        private string GetValue(object item)
        {
            if (item == null || item == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                var type = item.GetType(); 
                if (type == typeof(ProductUnit))
                {
                    return ((ProductUnit)item).NameUnit.ToString();
                }
                else if (type == typeof(ProductCodeUKTZED))
                {
                    return ((ProductCodeUKTZED)item).Code.ToString();
                }
                else if (type == typeof(Discount))
                {
                    return ((Discount)item).Rebate.ToString();
                }
                else
                {
                   return item.ToString();
                }
            }
        }

        public  DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            var dt = new DataTable(typeof(T).Name);

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var row = dt.NewRow();
                foreach (var prop in props)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

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
