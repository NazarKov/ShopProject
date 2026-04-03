using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.File.Directory.Interface
{
    internal interface IFileService
    {
        public string? Read(string path);
        public void Write(string path, string content);
        public bool IsCreateFile(string path);
    }
}
