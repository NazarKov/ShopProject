using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.ObjectOwner;
using ShopProject.Model.Domain.OperationRecorder; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.WorkingShift
{
    public class WorkingShiftStatus
    {
        public WorkingShift? WorkingShift { get; set; }
        public ObjectOwner.ObjectOwner? ObjectOwner { get; set; }
        public string? StatusShift { get; set; }
        public string? StatusOnline { get; set; }
        public OperationRecorder.OperationRecorder? OperationRecorder { get; set; }
        [JsonIgnore]
        public MediaAccessControl.MediaAccessControl? MediaAccessControl { get; set; }
        public string Serialize()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(this, options);
            return json;
        }
        public static WorkingShiftStatus? Deserialize(string jason)
        {
            if (jason != null && jason != string.Empty)
            {
                return JsonSerializer.Deserialize<WorkingShiftStatus>(jason);
            }
            else
            {
                return null;
            }
        }
    }
}
