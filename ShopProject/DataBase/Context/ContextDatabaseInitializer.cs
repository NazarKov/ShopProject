using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject.DataBase.Entities;

namespace ShopProject.DataBase.Context
{
    public class ContextDatabaseInitializer : DropCreateDatabaseIfModelChanges<ContextDataBase>
    {
        protected override void Seed(ContextDataBase context)
        {
            context.Users.Add(new UserEntiti() { Login = "Admin", Password = "Admin" ,CreatedAt = DateTime.Now, Status = 0 , UserRole = new UserRoleEntiti() { NameRole = "Admin", TypeAccess = 0 }});

            context.ProductUnits.Add(new ProductUnitEntiti() { Number = 2009, ShortNameUnit = "шт" , NameUnit = "Штука" });
            context.ProductUnits.Add(new ProductUnitEntiti() { Number = 0301, ShortNameUnit = "кг",  NameUnit = "Кілограм" });
            context.ProductUnits.Add(new ProductUnitEntiti() { Number = 2112, ShortNameUnit = "пач", NameUnit = "Пачка" });
            context.ProductUnits.Add(new ProductUnitEntiti() { Number = 2075, ShortNameUnit = "ящ" , NameUnit = "Ящик" });

            context.CodeUKTZED.Add(new CodeUKTZEDEntiti() { Code="9507" ,NameCode = "Вудки риболовні, гачки та інші снасті для риболовлі з використанням волосіні; сачки для риби, сачки для метеликів та подібні сачки; принади у вигляді муляжів птахів (крім включених до товарної позиції 9208 або 9705) та аналогічні вироби для полювання або стрільби" });
      
            context.SaveChanges();
            
        }
    }
}
