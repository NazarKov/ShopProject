using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model
{
    internal static class FileReaderCVS
    {
        public static bool writeFile(string path,List<Product> products)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(path, true,Encoding.UTF8))
                {
                    write.WriteLine("Штрихкод;Назва;Опис;Ціна;Продажна ціна;Кількість;Одиниці;Знижка;");
                    for (int i = 0; i < products.Count; i++)
                    {
                        write.WriteLine($"{products[i].code};{products[i].name};{products[i].description};{products[i].price};{products[i].purchase_prise};{products[i].count};{products[i].units};{products[i].sales};");
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
