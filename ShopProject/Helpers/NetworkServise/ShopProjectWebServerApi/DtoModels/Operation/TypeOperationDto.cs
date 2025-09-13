using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Operation
{
    public enum TypeOperationDto
    {
        FiscalCheck = 0,
        ReturnCheck = 1,
        DepositMoney = 2,
        WithdrawalMoney = 3,
        None,
    }
}
