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
        public static bool TextField(string name, string code, string articule, decimal price, decimal count, string units, bool validation)
        {
            if (validation)
            {
                ItemChekIsNull(name, typeof(string), "Назва").ToString();
                ItemChekIsNull(code, typeof(string), "Штрихкод").ToString();
                ItemChekIsNull(articule, typeof(string), "Артикуль").ToString();
                ItemChekIsNull(price, typeof(decimal), "Ціна");
                ItemChekIsNull(count, typeof(decimal), "Кількість"); ;
                ItemChekIsNull(units, typeof(string), "Одиниці").ToString();
                return true;
            }
            else
            {
                return true;
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
                else if (type.Equals(typeof(decimal)) && (decimal)item != 0)
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


        public static bool CodeCoincidenceinDatabase(string code , IEnumerable<Goods> products)
        {
            foreach (Goods item in products)
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
    }
}
