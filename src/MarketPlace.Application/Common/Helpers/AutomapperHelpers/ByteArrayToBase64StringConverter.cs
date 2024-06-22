using AutoMapper;

namespace MarketPlace.Application.Common.Helpers.AutomapperHelpers
{
    public class ByteArrayToBase64StringConverter : IValueConverter<byte[], string>
    {
        public string Convert(byte[] sourceMember, ResolutionContext context)
        {
            return sourceMember != null ? System.Convert.ToBase64String(sourceMember) : null;
        }
    }
}
