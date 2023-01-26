using ExcelDataReader;
using OfficeOpenXml;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace ShopProject.Model
{
    internal class FileExel
    {
        private DataTableCollection? tableCollection = null;

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
                    this.tableCollection = db.Tables;
                    stream.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Write(string path,List<Product> products)
        {
           //зробити
        }

        public DataTable? GetTabel(int i)
        {
            if (this.tableCollection != null)
                return this.tableCollection[i];
            else
                return null;
        }

        public List<string> GetTableName()
        {
            List<string> tableName = new List<string>();
            if(tableCollection!= null)
            foreach (DataTable table in tableCollection)
            {
                tableName.Add(table.TableName);
            }
            return tableName;
        }
    }
}
