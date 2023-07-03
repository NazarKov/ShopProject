using EUSignCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniServiceSigningFiles.Helpers.Command
{
    internal static class Command
    {
        public static string Initialize(bool modeUI)
        {
            int error = IEUSignCP.Initialize();
            if (error == IEUSignCP.EU_ERROR_NONE)
            {
                IEUSignCP.SetUIMode(modeUI);
                return "OK";
            }
            else
            {
                return IEUSignCP.GetErrorDesc(error);
            }
        }
        public static bool IsInitialize()
        {
            return IEUSignCP.IsInitialized();
        }
        public static bool SignFile()
        {
            if(IEUSignCP.IsInitialized())
            {
                IEUSignCP.SignFile("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\MiniServiceSigningFiles\\2.xml", "D:\\Проекти\\Visual Studio\\Project\\ShopProject\\MiniServiceSigningFiles\\2.xml.p7s", false);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Finalize()
        {
            IEUSignCP.Finalize();
        }
    }
}
