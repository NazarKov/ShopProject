using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Models.Domain.Setting;

namespace ShopProjectWebServer.Models.UI
{
    public class DataBaseModel
    {
        [BindProperty]
        public SettingDataBaseConnection SettingDataBaseConnection { get; set; } = new SettingDataBaseConnection();

        [BindProperty]
        public int Index { get; set; }
    }
}
