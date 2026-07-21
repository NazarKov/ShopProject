using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.WorkingShift
{
    internal class WorkingShiftStatusModel
    {
        public static List<string> GetWorkingShiftStatus()
        {
            return new List<string>()
            {
                "Не визначений",
                "Зміна відкрита",
                "Зміна закрита", 
            };
        } 
    }
}
