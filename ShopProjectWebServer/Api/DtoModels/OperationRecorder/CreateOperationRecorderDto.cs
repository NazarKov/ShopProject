using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.OperationRecorder
{
    public class CreateOperationRecorderDto
    {
        public string FiscalNumber { get; set; } = string.Empty; 
        public string LocalNumber { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
        public int TypeStatus { get; set; }
 
        public DateTimeOffset D_REG { get; set; }
  
        public string Address { get; set; } = string.Empty; 
   
    }
}
