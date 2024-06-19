using EUSignCP;
using MiniServiceSigningFiles.Helpers.TcpJsonRCP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniServiceSigningFiles.Helpers
{
    public static class CommandExecute
    {
        private static int _error;
        public static UserCommand Initialize(bool modeUI)
        {
            _error = IEUSignCP.Initialize();
            if (_error == IEUSignCP.EU_ERROR_NONE)
            {
                IEUSignCP.SetUIMode(modeUI);
                return new UserCommand() { Status = "100", Description = "OK", TypeCommand = TypeCommand.Initialize, PasswordKey = null, PathKey = null };
            }
            else
            {
                return new UserCommand() { Status = "404", Description = IEUSignCP.GetErrorDesc(_error), TypeCommand = TypeCommand.Error, PasswordKey = null, PathKey = null , Time = DateTime.Now};
            }

        }
        public static UserCommand IsInitialize()
        {
            if (IEUSignCP.IsInitialized())
            {
                return new UserCommand() { Status = "100", Description = "OK", TypeCommand = TypeCommand.IsInitialize, PasswordKey = null, PathKey = null, Time = DateTime.Now };
            }
            else
            {
                return new UserCommand() { Status = "404", Description = "ErrorInitialize", TypeCommand = TypeCommand.Error, PasswordKey = null, PathKey = null, Time = DateTime.Now };
            }
        }
        public static UserCommand SignFile(string pathKey , string passordKey)
        {
            if (IEUSignCP.IsInitialized())
            {
                IEUSignCP.EU_CERT_OWNER_INFO info = new IEUSignCP.EU_CERT_OWNER_INFO();
                _error =  IEUSignCP.ReadPrivateKeyFile(pathKey,passordKey, out info);

                if(_error == IEUSignCP.EU_ERROR_NONE)
                {
                    _error = IEUSignCP.SignFile("C:\\ProgramData\\ShopProject\\Temp\\Chek.xml", "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml.p7s", false);
                    if(_error == IEUSignCP.EU_ERROR_NONE)
                    {
                        return new UserCommand() { Status = "100", Description = "SignedSuccessfully", TypeCommand = TypeCommand.SingFile, PasswordKey = null, PathKey = null, Time = DateTime.Now };
                    }
                }
            }
            return new UserCommand() { Status = "404", Description = IEUSignCP.GetErrorDesc(_error), TypeCommand = TypeCommand.Error, PasswordKey = null, PathKey = null, Time = DateTime.Now };
        }
        public static UserCommand Ping()
        {
            return new UserCommand() { Status = "100", Description = "OK ", TypeCommand = TypeCommand.Ping, PasswordKey = null, PathKey = null, Time = DateTime.Now };
        }
       
        public static UserCommand GetDataKey(string pathKey, string passwordKey)
        {
            if (IEUSignCP.IsInitialized())
            {
                IEUSignCP.EU_CERT_OWNER_INFO info = new IEUSignCP.EU_CERT_OWNER_INFO();
                IEUSignCP.EU_CERT_INFO eU_CERT_INFO = new IEUSignCP.EU_CERT_INFO();

                _error = IEUSignCP.ReadPrivateKeyFile(pathKey, passwordKey, out info);
                if (_error == IEUSignCP.EU_ERROR_NONE)
                {   
                    _error = IEUSignCP.GetCertificateInfo(info.issuer, info.serial, out eU_CERT_INFO);
                    if (_error == IEUSignCP.EU_ERROR_NONE)
                    {
                        var error = CreateFile(eU_CERT_INFO.subjDRFOCode);
                      
                        if (error!=string.Empty)
                        {
                            return new UserCommand() { Status = "404", Description = error, TypeCommand = TypeCommand.Error, PasswordKey = null, PathKey = null, Time = DateTime.Now };
                        }

                        _error = IEUSignCP.SignFile("C:\\ProgramData\\ShopProject\\Temp\\Key.txt", "C:\\ProgramData\\ShopProject\\Temp\\Key.txt.p7s", false);
                        if (_error == IEUSignCP.EU_ERROR_NONE)
                        {
                            return new UserCommand() { Status = "100", Description = "SignedSuccessfully", TypeCommand = TypeCommand.SingFile, PasswordKey = null, PathKey = null, Time = DateTime.Now };
                        }
                    }
                }
            }
            return new UserCommand() { Status = "404", Description = IEUSignCP.GetErrorDesc(_error), TypeCommand = TypeCommand.Error, PasswordKey = null, PathKey = null, Time = DateTime.Now };
        }
        public static UserCommand Finalize()
        {
            IEUSignCP.Finalize();

            return new UserCommand() { Status = "100", Description = "OK", TypeCommand = TypeCommand.Finalize, PasswordKey = null, PathKey = null, Time = DateTime.Now };
        }
        private static string CreateFile(string dRFOCode)
        {
            try
            {
                using (FileStream fs = File.Create("C:\\ProgramData\\ShopProject\\Temp\\Key.txt"))
                {
                    fs.Dispose();
                }
                using (StreamWriter st = new StreamWriter("C:\\ProgramData\\ShopProject\\Temp\\Key.txt"))
                {
                    st.Write(dRFOCode);
                    st.Dispose();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
