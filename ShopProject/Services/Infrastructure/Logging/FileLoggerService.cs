using ShopProject.Services.Infrastructure.Logging.Interface;
using ShopProject.Services.Integration.Directory.Interface;
using ShopProject.Services.Integration.File.BaseFile.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Logging
{
    internal class FileLoggerService : ILoggerService
    {
        private string _pathLogFile;
        private IFileService _fileService;
        public FileLoggerService(IDirectoryService directoryService,IFileService fileService)
        {
            _fileService = fileService;
            directoryService.Init();
            _pathLogFile = directoryService.GetPathLog();
            _pathLogFile += "\\log_" + DateTime.Now.Date.ToShortDateString() + ".txt";
        }
        public void WriteLog(string message)
        {
            var log = _fileService.Read(_pathLogFile);
            log += "\n" + message;

            _fileService.Write(_pathLogFile, log);
        }
    }
}
