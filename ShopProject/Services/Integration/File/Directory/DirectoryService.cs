using ShopProject.Services.Integration.File.Directory.Interface;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.File.Directory
{
    internal class DirectoryService : IDirectoryService
    {
        private string _projectTitle;
        private readonly string _path;

        public DirectoryService() 
        {
            _projectTitle = string.Empty;
            _path = "C:\\ProgramData\\";
        }

        public void Init()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                _projectTitle = titleAttribute.Title;
            }
            else
            {
                throw new Exception("Невиявлено назви програми");
            }
        }
        public void CreateProgramFolders()
        {
            System.IO.Directory.CreateDirectory(_path + _projectTitle); 
            System.IO.Directory.CreateDirectory(_path + _projectTitle + "\\Temp");
            System.IO.Directory.CreateDirectory(_path + _projectTitle + "\\Setting");
            System.IO.Directory.CreateDirectory(_path + _projectTitle + "\\Log");
        }
        public bool IsCreateProgramFolders()
        {
            try
            {
                string[] directoryInfo = System.IO.Directory.GetDirectories(_path + _projectTitle);

                var isCreateTempFolder = directoryInfo.Contains(_path + _projectTitle + "\\Temp");
                var isCreateSettingFolder = directoryInfo.Contains(_path + _projectTitle + "\\Setting");
                var isCreateLogFolder = directoryInfo.Contains(_path + _projectTitle + "\\Log");
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
            return _path + _projectTitle + "\\Setting";
        }
        public string GetPathLog()
        {
            return _path + _projectTitle + "\\Log";
        } 
    }
}
