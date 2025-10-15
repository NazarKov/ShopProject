using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopProjectWebServer.Api.DtoModels.ObjectOwner
{
    public class ObjectOwnerListDto
    { 
        public string TypeObjectName { get; set; } = string.Empty; 
        public string NameObject { get; set; } = string.Empty; 
        public string CodeObject { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
        public int TypeStatus { get; set; } 
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
