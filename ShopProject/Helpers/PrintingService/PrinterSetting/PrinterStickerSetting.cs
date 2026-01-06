using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.PrintingService.PrinterSetting
{
    internal class PrinterStickerSetting
    {
        public string Printer { get; set; } = string.Empty; 
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static PrinterStickerSetting? Deserialize(string jason)
        {
            if (jason != null && jason != string.Empty)
            {
                return JsonSerializer.Deserialize<PrinterStickerSetting>(jason);
            }
            else
            {
                return null;
            }
        }
    }
}
