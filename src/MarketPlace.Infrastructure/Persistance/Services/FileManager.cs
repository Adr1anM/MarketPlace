using MarketPlace.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Persistance.Services
{
    public class FileManager : IFileManager
    {
        public byte[] ConvertFromBase64String(string base64Data)
        {
            if (string.IsNullOrEmpty(base64Data))
            {
                return null;
            }

            return Convert.FromBase64String(base64Data);
        }

        public string ConvertToBase64String(byte[] fileData)
        {
            if (fileData == null || fileData.Length == 0)
            {
                return null;
            }

            return Convert.ToBase64String(fileData);
        }

        public async Task<byte[]> ConvertToFileBytesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
