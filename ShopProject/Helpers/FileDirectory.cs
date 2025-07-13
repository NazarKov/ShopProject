using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers
{
    static class FileDirectory
    {
        private static string _projectTitle = string.Empty;
        private static readonly string _path = "C:\\ProgramData\\";

        public static void Init()
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
        public static void CreateProgramFolders()
        {
            Directory.CreateDirectory(_path + _projectTitle);
            Directory.CreateDirectory(_path + _projectTitle + "\\Users");
            Directory.CreateDirectory(_path + _projectTitle + "\\Temp");
        }
        public static void CreateUserFolder(string nameUser)
        {
            Directory.CreateDirectory(_path + _projectTitle + "\\Users\\" + nameUser);
            Directory.CreateDirectory(_path + _projectTitle + "\\Users\\" + nameUser + "\\Key");
        }
        public static string CopyKeyInUserFolder(string nameKey,string pathKey,string nameUser)
        {
            string path = _path + _projectTitle + "\\Users\\" + nameUser + "\\Key";
            path += "\\" + (Directory.GetFiles(path).Count() + 1) + "." + nameKey;
            File.Copy(pathKey, path);
            return path;
        }

        public static bool IsCreateProgramFolders()
        {
            try
            {
                string[] directoryInfo =  Directory.GetDirectories(_path + _projectTitle);
                return true;
            }
            catch 
            {
                return false;
            }
        }

    }
}
