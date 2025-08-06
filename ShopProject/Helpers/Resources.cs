using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers
{
    public static class Resources
    {
        public static void Init()
        {
            InitWebServer();
            InitSystemFolders();
        }

        private static void InitSystemFolders()
        {
            FileDirectory.Init();
            if (!FileDirectory.IsCreateProgramFolders())
            {
                FileDirectory.CreateProgramFolders();
            }
        }
        private static void InitWebServer()
        {
            MainWebServerController.Init();
        }
    }
}
