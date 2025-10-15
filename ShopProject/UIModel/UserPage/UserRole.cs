using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.UserPage
{
    public class UserRole
    {
        public int ID { get; set; }
        public string NameRole { get; set; } = string.Empty; 
        public int TypeAccess { get; set; } = 0;
    }
}
