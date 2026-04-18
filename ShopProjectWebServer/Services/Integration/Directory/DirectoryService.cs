using ShopProjectWebServer.Service.Integration.Directory.Interface;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Service.Integration.Directory
{
    public class DirectoryService : IDirectoryService
    {
        private readonly string _solutionName = "ShopProject"; 
        private readonly string _path;

        public DirectoryService() 
        { 
            _path = GetSolutionPath(); 
        }

        public string GetSolutionPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), _solutionName);
        }
        public void CreateProgramFolders()
        {
            System.IO.Directory.CreateDirectory(_path); 
            System.IO.Directory.CreateDirectory(_path + "\\Temp");
            System.IO.Directory.CreateDirectory(_path + "\\Setting");
            System.IO.Directory.CreateDirectory(_path + "\\LogServer");
        }
        public bool IsCreateProgramFolders()
        {
            try
            {
                string[] directoryInfo = System.IO.Directory.GetDirectories(_path);

                var isCreateTempFolder = directoryInfo.Contains(_path + "\\Temp");
                var isCreateSettingFolder = directoryInfo.Contains(_path + "\\Setting");
                var isCreateLogFolder = directoryInfo.Contains(_path + "\\LogServer");
                if (isCreateTempFolder && isCreateTempFolder&& isCreateLogFolder) 
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public string GetPathSetting()
        {
            return _path + "\\Setting";
        }
        public string GetPathLog()
        {
            return _path + "\\LogServer";
        } 
    }
}
