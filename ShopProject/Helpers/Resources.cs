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
        public static void init()
        {
            MainWebServerController.Init();
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
    }
}
