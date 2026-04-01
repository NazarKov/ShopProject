using ShopProject.Resource.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Printing.Interface
{
    internal interface IPrintingFiscalCheckService
    {
        public void PrintCheck(TemplatePrintingCheck check);
    }
}
