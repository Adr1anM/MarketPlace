using MarketPlace.Application.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.FileSystem
{
    public class FileService : IFileService
    {
        private const string DirectoryPath = @"D:\Amdaris\InternshipProject\MarketPlace\FilesWithLogs";
        private string Filename = $"Logs_{DateTime.Now:yyyyMMdd}.txt";

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
            string filePath = Path.Combine(DirectoryPath, Filename);

            await Task.Run(() => Directory.CreateDirectory(DirectoryPath));


            using (StreamWriter writer = File.AppendText(filePath))
            {
               await writer.WriteLineAsync(message);
            }
        }
    }
}
