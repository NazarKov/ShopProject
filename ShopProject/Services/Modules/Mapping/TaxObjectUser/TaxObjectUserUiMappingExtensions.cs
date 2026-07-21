using ShopProject.Model.UI.PointOfSale;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.Services.Modules.Mapping.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.TaxObjectUser
{
    internal static class TaxObjectUserUiMappingExtensions
    {
        public static IEnumerable<TaxObjectAndOperationRecorderModel> ToTaxObjectAndOperationRecorderModel(this IEnumerable<ShopProject.Model.Domain.TaxObjectUser.TaxObjectUser> items)
        {
            var result = new List<TaxObjectAndOperationRecorderModel>();
            foreach(var item in items)
            {
                var temp = new TaxObjectAndOperationRecorderModel();
                if(item.TaxObject != null)
                {
                    temp.TaxObject = item.TaxObject.ToTaxObjectModel();
                }
                if (item.OperationRecorders != null)
                {
                    temp.OperationRecorders = new List<ShopProject.Model.UI.OperationRecorder.OperationRecorderModel>(item.OperationRecorders.ToOperationRecorderModel());
                } 
                result.Add(temp);
            }
            return result;
        }
    }
}
