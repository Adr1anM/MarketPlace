using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Common.Helpers.AutomapperHelpers
{
    public class ConvertToBase64String
    {
        public static string Convevrt(byte[] imageData)
        {
            return imageData != null ? Convert.ToBase64String(imageData) : null;
        }
    }
}
