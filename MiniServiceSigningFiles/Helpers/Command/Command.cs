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
                IEUSignCP.EU_CERT_OWNER_INFO info = new IEUSignCP.EU_CERT_OWNER_INFO();
                IEUSignCP.ReadPrivateKeyFile("F:\\key_13100560_13100560.jks", "1234567zZ",out info);
                IEUSignCP.SignFile("C:\\Users\\Nazar\\Source\\Repos\\NazarKov\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml", "C:\\Users\\Nazar\\Source\\Repos\\NazarKov\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml.p7s", false);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool None()
        {

            if( IEUSignCP.IsInitialized())
            {
                
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
