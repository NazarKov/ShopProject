using ShopProject.Services.Integration.File.BaseFile.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.File.BaseFile
{
    internal class FileService : IFileService
    {
        public string? Read(string path)
        {
            return System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path) : null;
        }

        public void Write(string path, string content)
        {
            System.IO.File.WriteAllText(path, content);
        }

        public bool IsCreateFile(string path) 
        {
            if (System.IO.File.Exists(path)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
