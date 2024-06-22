using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Services
{
    public interface IFileManager
    {
        Task<byte[]> ConvertToFileBytesAsync(IFormFile file);
        string ConvertToBase64String(byte[] bytes);
        byte[] ConvertFromBase64String(string base64Data);
    }
}
