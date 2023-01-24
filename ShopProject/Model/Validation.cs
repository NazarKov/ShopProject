using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model
{
    internal static class Validation
    {
        public static void TextField(Product product,string name, string code, string articule, double price, double statingPrice, int count, string units, bool validation)
        {
            if (validation)
            {
                if (product != null)
                {
                    product.name = ItemChekIsNull(name, typeof(string), "Назва").ToString();
                    product.code = ItemChekIsNull(code, typeof(string), "Штрихкод").ToString();
                    product.articule = ItemChekIsNull(articule, typeof(string), "Артикуль").ToString();
                    product.price = Convert.ToDouble(ItemChekIsNull(price, typeof(double), "Ціна"));
                    product.startingPrise = Convert.ToDouble(ItemChekIsNull(statingPrice, typeof(double), "Початкова ціна"));
                    product.count = Convert.ToInt32(ItemChekIsNull(count, typeof(int), "Кількість")); ;
                    product.units = ItemChekIsNull(units, typeof(string), "Одиниці").ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                    product.markUp = (price / statingPrice) * 100;
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
                    product.startingPrise = statingPrice;
                    product.count = count;
                    product.units = units.ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                    product.markUp = (price / statingPrice) * 100;
                }
            }
        }


        private static object ItemChekIsNull(object? item, Type type, string messeges)
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
    }
}
