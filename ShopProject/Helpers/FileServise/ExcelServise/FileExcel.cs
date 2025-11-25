using ClosedXML.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.FileServise.ExcelServise
{
    public static class FileExcel
    {
        public static DataTableCollection Read(string path )
        { 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DataSet db;
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader read = ExcelReaderFactory.CreateReader(stream);

                db = read.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                stream.Close(); 
            } 
            return db.Tables;
        }
        public static void Write(string path, XLWorkbook workbook) 
        { 
            workbook.SaveAs(path); 
        }
    }
}
