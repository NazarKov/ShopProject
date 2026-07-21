 
namespace ShopProjectWebServer.Models.Domain.TaxObjectUser
{
    public class TaxObjectUser
    { 
        public int ID { get; set; }

        public ShopProjectWebServer.Models.Domain.User.User? User { get; set; }
        public ShopProjectWebServer.Models.Domain.TaxObject.TaxObject? TaxObject { get; set; } 
        public IEnumerable<ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder>? OperationRecorder { get; set; }
    }
}
