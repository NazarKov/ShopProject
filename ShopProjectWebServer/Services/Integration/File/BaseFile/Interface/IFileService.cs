using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Service.Integration.File.BaseFile.Interface
{
    public interface IFileService
    {
        public string? Read(string path);
        public void Write(string path, string content);
        public bool IsCreateFile(string path);
    }
}
