using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Persistance.Services
{
    public class FormFileByteArrayConverter : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
        {
            using var memoryStream = new MemoryStream();
            source.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
