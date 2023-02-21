using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace ShopProject.Model
{
    internal static class Validation
    {
        public static void TextField(Product product,string name, string code, string articule, double price, int count, string units, bool validation)
        {
            if (validation)
            {
                if (product != null)
                {
                    product.name = ItemChekIsNull(name, typeof(string), "Назва").ToString();
                    product.code = ItemChekIsNull(code, typeof(string), "Штрихкод").ToString();
                    product.articule = ItemChekIsNull(articule, typeof(string), "Артикуль").ToString();
                    product.price = Convert.ToDouble(ItemChekIsNull(price, typeof(double), "Ціна"));
                    product.count = Convert.ToInt32(ItemChekIsNull(count, typeof(int), "Кількість")); ;
                    product.units = ItemChekIsNull(units, typeof(string), "Одиниці").ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                }
            }
            else
            {
                if (product != null)
                {
                    product.name = name.ToString();
                    product.code = code.ToString();
                    product.articule = articule.ToString();
                    product.price = price;
                    product.count = count;
                    product.units = units.ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                }
            }
        }

        public static object ItemChekIsNull(object? item, Type type, string messeges)
        {
            if (item != null)
            {
                if (type.Equals(typeof(string)) && (string)item != string.Empty)
                {
                    return item;
                }
                else if (type.Equals(typeof(int)) && (int)item != 0)
                {
                    return item;
                }
                else if (type.Equals(typeof(double)) && (double)item != 0)
                {
                    return item;
                }
                else
                {
                    throw new Exception("Заповніть поле " + messeges);
                }
            }
            else
            {
                throw new Exception("Заповніть поле " + messeges);
            }

        }
        public static string ChekParamsIsNull(int column, int rows, DataTable table)
        {
            if (column != 0)
            {
                return table.Rows[rows].ItemArray.ElementAt(column - 1).ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public static int ChekNull(int item)
        {
            if(item != 0)
            {
                return item;
            }
            else
            {
                return 0;
            }
        }
        public static int ChekNull(int item,int count)
        {
            if (item != 0)
            {
                return item;
            }
            else
            {
                return count;
            }
        }
        
        public static double ChekEmpty(int item,int i , DataTable dataTable)
        {
            if (ChekParamsIsNull(item, i, dataTable) != string.Empty)
            {
                return Convert.ToDouble(ChekParamsIsNull(item, i, dataTable));
            }
            else
            {
                return 0;
            }
        }


        public static bool CodeCoincidenceinDatabase(string code , ShopContext db)
        {
            if (db != null)
                if (db.products != null)
                    foreach (Product item in db.products)
                    {
                        if (item.code == code)
                        {
                            return true;
                        }
                    }
            return false;
        }

        public static void ChekRowIsNull(int code, int name, int articule, int price, int count, int units)
        {
            if(code == 0)
            {
                if(name==0)
                {
                    if(articule==0)
                    {
                        if(price==0)
                        {   
                            if (count == 0)
                            {
                                if(units==0)
                                {
                                    throw new Exception("Заповніть поля");
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void ChekIsProductInArhive(Product product,List<Archive> archives)
        {
            foreach (Archive archive in archives)
            {
                if (archive.product != null)
                {
                    if (archive.product.Equals(product))
                    {
                        throw new Exception("Товар вже в архіві");
                    }
                }
            }
        }
    
    }
}
