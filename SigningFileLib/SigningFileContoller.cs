using EUSignCP;
using SigningFileLib.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigningFileLib
{
    public class SigningFileContoller
    {
        private int _error;

        private readonly string fileName = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml";
        private readonly string signedFileName = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml.p7s";


        public void Initialize(bool modeUI)
        {
            _error = IEUSignCP.Initialize();
            IEUSignCP.SetUIMode(modeUI);
            IEUSignCP.SetRuntimeParameter(IEUSignCP.EU_FP_RESET,true);
            GetError(_error);

        }
        public OperationResult<string> GetError(int error)
        {
            if(error == IEUSignCP.EU_ERROR_NONE)
            {
                return OperationResult<string>.Success("");
            }

            return OperationResult<string>.Fail(IEUSignCP.GetErrorDesc(error));
        } 

        public int SignFileToByteKey(byte[] key , string password)
        { 
            if (IEUSignCP.IsInitialized())
            {
                var info = new IEUSignCP.EU_CERT_OWNER_INFO();
                IEUSignCP.ReadPrivateKeyBinary(key, password, out info);


                if (_error == IEUSignCP.EU_ERROR_NONE)
                {
                    return IEUSignCP.SignFile(fileName, signedFileName, false); 
                }
                else
                {
                    return _error;
                }
            }
            return IEUSignCP.EU_ERROR_NOT_INITIALIZED;
        } 

        public int GetDataToFile(string pathKey, string passwordKey)
        {
            if (IEUSignCP.IsInitialized())
            {
                IEUSignCP.EU_CERT_OWNER_INFO info = new IEUSignCP.EU_CERT_OWNER_INFO();
                IEUSignCP.EU_CERT_INFO eU_CERT_INFO = new IEUSignCP.EU_CERT_INFO();

                _error = IEUSignCP.ReadPrivateKeyFile(pathKey, passwordKey, out info);
                if(_error != IEUSignCP.EU_ERROR_NONE)
                {
                    return _error;
                } 

                _error = IEUSignCP.GetCertificateInfo(info.issuer, info.serial, out eU_CERT_INFO);
                if (_error != IEUSignCP.EU_ERROR_NONE)
                {
                    return _error;
                }
                CreateFile(eU_CERT_INFO.subjDRFOCode);


                return IEUSignCP.SignFile("C:\\ProgramData\\ShopProject\\Temp\\Key.txt", "C:\\ProgramData\\ShopProject\\Temp\\Key.txt.p7s", false); 
            }
            return IEUSignCP.EU_ERROR_NOT_INITIALIZED;
        }

        public OperationResult<IEUSignCP.EU_CERT_OWNER_INFO> GetDataOwner(string pathKey, string passwordKey)
        {
            if (IEUSignCP.IsInitialized())
            {
                IEUSignCP.EU_CERT_OWNER_INFO info = new IEUSignCP.EU_CERT_OWNER_INFO(); 
                _error = IEUSignCP.ReadPrivateKeyFile(pathKey, passwordKey, out info);
                if (_error == IEUSignCP.EU_ERROR_NONE)
                {
                    return OperationResult<IEUSignCP.EU_CERT_OWNER_INFO>.Success(info); 
                }  
            }
            return OperationResult<IEUSignCP.EU_CERT_OWNER_INFO>.Fail(IEUSignCP.GetErrorDesc(_error)); 
        }

        public void Finalize() => IEUSignCP.Finalize();
        
        private void CreateFile(string dRFOCode)
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
