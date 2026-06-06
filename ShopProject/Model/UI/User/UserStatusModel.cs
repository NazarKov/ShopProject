using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.User
{
    class UserStatusModel
    {
        public static List<string> GetUserStatus()
        {
            return new List<string>()
            {
                "Не визначений",
                "Цийфровий підпис завантажений",
                "Цифровий підпис відсутній", 
            };
        }
        public static List<string> GetUserStatusForStorage()
        {
            return new List<string>()
            {
                "Статус (Всі)",
                "Статус (З цифровим підписом)",
                "Статус (Без цифрового підпису)", 
            };
        }
    }
}
