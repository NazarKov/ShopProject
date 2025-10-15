using ShopProjectDataBase.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopProjectWebServer.Api.DtoModels.OperationRecorderUser
{
    public class OperationRecorderUserDto
    { 
        public Guid ID { get; set; } 
        public Guid? UserID { get; set; }
        public int? OpertionsRecordersID { get; set; }
    }
}
