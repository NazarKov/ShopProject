using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Helpers.Settings;

namespace ShopProjectWebServer.Models
{
    public class DataBaseViewModel
    {
        [BindProperty]
        public SettingDataBase DataBase { get; set; }

        [BindProperty]
        public int index { get; set; }
    }
}
