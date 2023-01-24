using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model
{
    internal class FileExel
    {
        private DataTableCollection? tableCollection = null;

        public FileExel(string filePath)
        {
            readFile(filePath);
        }

        public void readFile(string path)
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
            foreach (DataTable table in tableCollection)
            {
                tableName.Add(table.TableName);
            }
            return tableName;
        }
    }
}
