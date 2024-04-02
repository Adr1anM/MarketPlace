using MarketPlace.Application.FileServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.FileSystem
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private static DateTime currTime;
        
        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
      
        public async Task<string> GetFile(string path)
        {
           
            if(File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                   var result = await reader.ReadToEndAsync(); 
                   return result;
                }
            }

            return "EMPTY";
            
        }

        public async Task SaveToFile(string message)
        {
            string Filename = $"Logs_{currTime:yyyyMMdd}.txt";

            string path = _configuration.GetValue<string>("FileDirectories:LogFilePath")!;
            string filePath = Path.Combine(path, Filename);
            

            await Task.Run(() => Directory.CreateDirectory(path));


            using (StreamWriter writer = File.AppendText(filePath))
            {
               await writer.WriteLineAsync(message);
            }
        }
    }
}
