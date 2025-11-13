using ShopProject.Helpers;
using ShopProject.Helpers.FileServise.ExcelServise;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage.ExcelPage.ImportExcelPage
{
    internal class ImportProductExelModel 
    {

        private FileExcelServise _fileExelServise; 
        private string path; 

        public ImportProductExelModel()
        {  
            path = string.Empty;
            _fileExelServise = new FileExcelServise();
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
        public bool LoadFile()
        {
            try
            {
                _fileExelServise = new FileExcelServise(FileExcel.Read(path));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
        public DataTable GetTable(int i) => _fileExelServise.GetTabel(i); 
        public List<string> GetTableName() => _fileExelServise.GetTableName(); 
        public List<string> GetTableHeaders(int i) => _fileExelServise.GetHeaders(i); 
        public int GetMaxRow(int i)=>_fileExelServise.GetMaxRow(i);

        public async Task<bool> SaveItem(int sheet, SelectExcelColumn selectExcelColumn, int topRow, int bottomRow)
        {
            List<Product> products = new List<Product>();
            int i = topRow;

            try
            {
                for (; i < bottomRow; i++)
                {

                    if (_fileExelServise != null)
                    {
                        var headers = GetTableHeaders(sheet).Count;

                        var product = new Product() { };
                        var barcode = _fileExelServise.GetValue(i, selectExcelColumn.ColumnBarCode - 1, sheet, (selectExcelColumn.ColumnBarCode != headers + 1));
                        if (barcode != null)
                        {
                            product.Code = barcode.ToString();
                        }
                        else
                        {
                            product.Code = string.Empty;
                        }
                        var NameProduct = _fileExelServise.GetValue(i, selectExcelColumn.ColumnName - 1, sheet, (selectExcelColumn.ColumnName != headers + 1));
                        if (NameProduct != null)
                        {
                            product.NameProduct = NameProduct.ToString();
                        }
                        else
                        {
                            product.NameProduct = string.Empty;
                        }
                        var Articule = _fileExelServise.GetValue(i, selectExcelColumn.ColumnArticule - 1, sheet, (selectExcelColumn.ColumnArticule != headers + 1));
                        if (Articule != null)
                        {
                            product.Articule = Articule.ToString();
                        }
                        else
                        {
                            product.Articule = string.Empty;
                        }
                        var Price = _fileExelServise.GetValue(i, selectExcelColumn.ColumnPrice - 1, sheet, (selectExcelColumn.ColumnPrice != headers + 1));
                        if (Price != null)
                        { 
                            product.Price = TryToDecimal(Price);
                        }
                        else
                        {
                            product.Price = 0;
                        }
                        var Count = _fileExelServise.GetValue(i, selectExcelColumn.ColumnCount - 1, sheet, (selectExcelColumn.ColumnCount != headers + 1));
                        if (Count != null)
                        {
                            product.Count = TryToDecimal(Count);
                        }
                        else
                        {
                            product.Count = 0;
                        }
                        //var discount = _fileExelServise.GetValue(i, selectExcelColumn.ColumnDiscount, sheet).ToString();
                        //if (discount != null)
                        //{
                        //    product.Discount = new Discount();
                        //}
                        var unit = _fileExelServise.GetValue(i, selectExcelColumn.ColumnUnit - 1, sheet, (selectExcelColumn.ColumnUnit != headers + 1));
                        if (unit != null)
                        {

                            var item = Session.ProductUnits.FirstOrDefault(u => u.NameUnit.ToLower() == unit.ToString().ToLower());

                            if (item == null)
                            {
                                item = Session.ProductUnits.FirstOrDefault(u => u.ShortNameUnit.ToLower() == unit.ToString().ToLower());
                            }

                            if (item != null)
                            {
                                product.Unit = item;
                            }
                            else
                            {
                                product.Unit = null;
                            }
                        }
                        var code = _fileExelServise.GetValue(i, selectExcelColumn.ColumnCodeUKTZED - 1, sheet, (selectExcelColumn.ColumnCodeUKTZED != headers + 1));
                        if (code != null)
                        {
                            var item = Session.ProductCodesUKTZED.FirstOrDefault(c => c.Code == code.ToString());
                            if (item != null)
                            {
                                product.CodeUKTZED = item;
                            }
                            else
                            {
                                product.CodeUKTZED = null;
                            }
                        }
                        product.CreatedAt = DateTime.Now;
                        product.Status = TypeStatusProduct.ImportedExcel;
                        products.Add(product);
                    }
                }

                return await MainWebServerController.MainDataBaseConntroller.ProductController.AddProductRange(Session.User.Token, products);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        decimal TryToDecimal(object value)
        {
            try
            {
                if (value == null)
                    return decimal.Zero;

                string s = value.ToString().Trim();

                if (s.Contains('.') && !s.Contains(','))
                    s = s.Replace('.', ',');
                return decimal.Parse(s);
            }
            catch 
            {
                return decimal.Zero;
            } 
        }
    }
}
