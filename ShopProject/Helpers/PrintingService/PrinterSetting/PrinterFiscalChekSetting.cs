using ShopProject.Resource.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.PrintingService.PrinterSetting
{
    internal class PrinterFiscalChekSetting
    {
        public string Printer { get; set; } = string.Empty;
        public double Width { get; set; } = 0; 
        public double Slcale { get; set; } = 0; 
        public double SizeQrCode {  get; set; } = 0;
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static PrinterFiscalChekSetting? Deserialize(string jason)
        {
            if (jason != null && jason != string.Empty)
            {
                return JsonSerializer.Deserialize<PrinterFiscalChekSetting>(jason);
            }
            else
            {
                return null;
            }
        }
    }
}
