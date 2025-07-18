using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProjectDataBase.DataBase.Entities;

namespace ShopProjectDataBase.DataBase.Context
{
    public class ContextDatabaseInitializer : DropCreateDatabaseIfModelChanges<ContextDataBase>
    {
        protected override void Seed(ContextDataBase context)
        {
            context.Users.Add(new UserEntity() { Login = "Admin", Password = "Admin" ,CreatedAt = DateTime.Now, Status = 0 , UserRole = new UserRoleEntity() { NameRole = "Admin", TypeAccess = 0 }});

            context.ProductUnits.Add(new ProductUnitEntity() { Number = 2009, ShortNameUnit = "шт" , NameUnit = "Штука", Status =  TypeStatusUnit.UnFavorite });
            context.ProductUnits.Add(new ProductUnitEntity() { Number = 0301, ShortNameUnit = "кг",  NameUnit = "Кілограм" , Status =  TypeStatusUnit.UnFavorite });
            context.ProductUnits.Add(new ProductUnitEntity() { Number = 2112, ShortNameUnit = "пач", NameUnit = "Пачка" , Status =  TypeStatusUnit.UnFavorite });
            context.ProductUnits.Add(new ProductUnitEntity() { Number = 2075, ShortNameUnit = "ящ" , NameUnit = "Ящик" , Status =  TypeStatusUnit.UnFavorite });

            context.ProductCodeUKTZED.Add(new ProductCodeUKTZEDEntity() { Code="9507" , Status =  TypeStatusCodeUKTZED.UnFavorite ,NameCode = "Вудки риболовні, гачки та інші снасті для риболовлі з використанням волосіні; сачки для риби, сачки для метеликів та подібні сачки; принади у вигляді муляжів птахів (крім включених до товарної позиції 9208 або 9705) та аналогічні вироби для полювання або стрільби" });
      
            context.SaveChanges();
            
        }
    }
}
