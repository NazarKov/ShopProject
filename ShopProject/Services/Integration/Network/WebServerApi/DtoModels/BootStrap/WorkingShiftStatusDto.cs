using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.WorkingShift;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObject;
using System;
using System.Text.Json.Serialization;

namespace ShopProject.Services.Integration.Network.WebServerApi.DtoModels.BootStrap
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
