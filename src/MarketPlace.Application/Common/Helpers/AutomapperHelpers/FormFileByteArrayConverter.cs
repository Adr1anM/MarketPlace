using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Common.Helpers.AutomapperHelpers
{
    public class FormFileByteArrayValueConverter : IValueConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile source, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                source.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
}
