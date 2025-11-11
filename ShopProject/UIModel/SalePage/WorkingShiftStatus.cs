using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json; 

namespace ShopProject.UIModel.SalePage
{
    public class WorkingShiftStatus
    {
        public WorkingShift? WorkingShift { get; set; }
        public string? StatusShift { get; set; }
        public string StatusOnline { get; set; }
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
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
