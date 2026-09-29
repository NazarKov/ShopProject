using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.TaxObject;
using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.BootStrap
{
    public class WorkingShiftStatusDto
    {
        [JsonPropertyName("UserID")]
        public Guid UserID { get; set; }
        [JsonPropertyName("WorkingShift")]
        public WorkingShiftDto? WorkingShift { get; set; }
        [JsonPropertyName("TaxObject")]
        public TaxObjectDto TaxObject { get; set; } = new TaxObject.TaxObjectDto();
        [JsonPropertyName("Status")]
        public int Status { get; set; }
        [JsonPropertyName("OpenShiftTime")]
        public DateTime? OpenShiftTime { get; set; }
        [JsonPropertyName("OperationRecorder")]
        public OperationRecorderDto OperationRecorder { get; set; } = new OperationRecorderDto();
    }
}
