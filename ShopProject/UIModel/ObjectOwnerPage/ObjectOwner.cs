using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.ObjectOwnerPage
{
    public class ObjectOwner
    {
        public Guid ID { get; set; } 
        public string TypeObjectName { get; set; } = string.Empty; 
        public string NameObject { get; set; } = string.Empty; 
        public string CodeObject { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
        public TypeStatusObjectOwner TypeStatus { get; set; } 
        public string TypeOfRights { get; set; } = string.Empty; 
        public DateTimeOffset? D_ACC_START { get; set; } 
        public DateTimeOffset? D_ACC_END { get; set; } 
        public string C_DISTR { get; set; } = string.Empty; 
        public DateTimeOffset? D_LAST_CH { get; set; } 
        public string C_TERRIT { get; set; } = string.Empty; 
        public string? REG_NUM_OBJ { get; set; } 
        public string KATOTTG { get; set; } = string.Empty; 
    }
}
