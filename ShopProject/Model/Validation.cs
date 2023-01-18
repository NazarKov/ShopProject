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
        public static void TextField(Product product,string name, string code, string articule, string description, double price, double purchase_price, int count, string units, bool validation)
        {
            if (validation)
            {
                if (product != null)
                {
                    product.name = ItemChekIsNull(name, typeof(string), "Назва").ToString();
                    product.code = ItemChekIsNull(code, typeof(string), "Штрихкод").ToString();
                    product.articule = ItemChekIsNull(articule, typeof(string), "Артикуль").ToString();
                    product.description = ItemChekIsNull(description, typeof(string), "Опис").ToString();
                    product.price = Convert.ToDouble(ItemChekIsNull(price, typeof(double), "Ціна"));
                    product.purchase_prise = Convert.ToDouble(ItemChekIsNull(purchase_price, typeof(double), "Початкова ціна"));
                    product.count = Convert.ToInt32(ItemChekIsNull(count, typeof(int), "Кількість")); ;
                    product.units = ItemChekIsNull(units, typeof(string), "Одиниці").ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                    product.mark_up = (price / purchase_price) * 100;
                }
            }
            else
            {
                if (product != null)
                {
                    product.name = name.ToString();
                    product.code = code.ToString();
                    product.articule = articule.ToString();
                    product.description = description.ToString();
                    product.price = price;
                    product.purchase_prise = purchase_price;
                    product.count = count;
                    product.units = units.ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                    product.mark_up = (price / purchase_price) * 100;
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
