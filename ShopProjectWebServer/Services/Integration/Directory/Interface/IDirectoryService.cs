using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Service.Integration.Directory.Interface
{
    public interface IDirectoryService
    { 
        public void CreateProgramFolders(); 
        public bool IsCreateProgramFolders();
        public string GetPathSetting();
        public string GetPathLog();
    }
}
