using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.Context
{
    public class ContextDatabaseInitializer : DropCreateDatabaseIfModelChanges<ContextDataBase>
    {
        protected override void Seed(ContextDataBase context)
        {
            context.goodsUnits.Add(new GoodsUnit() { number = 2009, name = "шт" , shortName = "Штука" });
            context.goodsUnits.Add(new GoodsUnit() { number = 0301, name = "кг", shortName = "Кілограм" });
            context.goodsUnits.Add(new GoodsUnit() { number = 2112, name = "пач", shortName = "Пачка" });
            context.goodsUnits.Add(new GoodsUnit() { number = 2075, name = "ящ" , shortName = "Ящик" });

            context.SaveChanges();

            context.codeUKTZED.Add(new CodeUKTZED() { code="9507" ,name = "Вудки риболовні, гачки та інші снасті для риболовлі з використанням волосіні; сачки для риби, сачки для метеликів та подібні сачки; принади у вигляді муляжів птахів (крім включених до товарної позиції 9208 або 9705) та аналогічні вироби для полювання або стрільби" });
      
            context.SaveChanges();
            
        }
    }
}
