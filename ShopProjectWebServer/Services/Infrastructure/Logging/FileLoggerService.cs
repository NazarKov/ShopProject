using ShopProjectWebServer.Service.Integration.Directory.Interface;
using ShopProjectWebServer.Service.Integration.File.BaseFile.Interface;
using ShopProjectWebServer.Services.Infrastructure.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Services.Infrastructure.Logging
{
    public class FileLoggerService : ILoggerService
    {
        private string _pathLogFile;
        private IFileService _fileService;
        public FileLoggerService(IDirectoryService directoryService,IFileService fileService)
        {
            _fileService = fileService;
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
