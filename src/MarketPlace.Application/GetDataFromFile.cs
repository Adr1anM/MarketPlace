
using MarketPlace.Application.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application
{
    public class GetDataFromFile
    {
        private readonly IFileService _fileService;

        public GetDataFromFile(IFileService fileservice)
        {
            _fileService =  fileservice;
        }

        public async Task<bool> CompareTofileData(string template)
        {
            string path = @"C:\Users\andri\OneDrive\Desktop\TestFile.txt";
            string data = await _fileService.GetFile(path);
            
            if(data.Contains(template))
            {
                return true;
            }

            return false;
        }
    }
}
