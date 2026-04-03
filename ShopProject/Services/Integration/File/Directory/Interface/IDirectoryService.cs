using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.File.Directory.Interface
{
    internal interface IDirectoryService
    {
        public void Init();
        public void CreateProgramFolders(); 
        public bool IsCreateProgramFolders();
        public string GetPathSetting();
        public string GetPathLog();
    }
}
