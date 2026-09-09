using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Modules.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface
{
    internal interface ISaleMenuService
    {
        public Task<OperationResult<bool>> SendCheck(OperationSaleInfo operationSaleInfo); 
    }
}
