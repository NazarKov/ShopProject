using ShopProject.DataBase.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model
{
    internal static class FileCVS
    {
        public static bool writeFile(string path,List<Product> products)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(path, true,Encoding.UTF8))
                {
                    write.WriteLine("Штрихкод;Назва;Ціна;Продажна ціна;Кількість;Одиниці;Знижка;");
                    for (int i = 0; i < products.Count; i++)
                    {
                        write.WriteLine($"{products[i].code};{products[i].name};{products[i].price};{products[i].startingPrise};{products[i].count};{products[i].units};{products[i].sales};");
                    }
                }
                return true;
            }
            catch
            {
                return false; 
            }
        }
    }
}
