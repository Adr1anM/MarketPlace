using MarketPlace.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarketPlace.Application.FileServices;

namespace MarketPlace.Infrastructure.FileSystem
{
    public class FileLogger : IFileLogger
    {
        private readonly IFileService _fileService;
        public FileLogger(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task LogSuccess(string message)
        {
            string logMessage = $"[{DateTime.UtcNow}] SUCCESS: {message}";
            await _fileService.SaveToFile(logMessage);
        }

        public async Task LogFailure(string message)
        {
            string logMessage = $"[{DateTime.UtcNow}] FAILURE: {message}";
            await _fileService.SaveToFile(logMessage);
        
        }
    }
}
