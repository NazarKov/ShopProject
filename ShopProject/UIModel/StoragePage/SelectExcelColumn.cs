using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.StoragePage
{
    public class SelectExcelColumn
    {
        public int ColumnBarCode { get; set; }
        public int ColumnName { get; set; }
        public int ColumnArticule { get; set; }
        public int ColumnPrice { get; set; }
        public int ColumnCount { get; set; }
        public int ColumnDiscount { get; set; }
        public int ColumnUnit { get; set; }
        public int ColumnCodeUKTZED {  get; set; }

        public SelectExcelColumn()
        {
            ColumnBarCode = 0;
            ColumnName = 0;
            ColumnArticule = 0;
            ColumnPrice = 0;
            ColumnCount = 0;
            ColumnDiscount = 0;
            ColumnUnit = 0;
            ColumnCodeUKTZED = 0;
        }

        public void Clear()
        {
            ColumnBarCode = 0;
            ColumnName = 0;
            ColumnArticule = 0;
            ColumnPrice = 0;
            ColumnCount = 0;
            ColumnDiscount = 0;
            ColumnUnit = 0;
            ColumnCodeUKTZED = 0;
        }
    }
}
