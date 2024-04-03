using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class Paint : BaseArtProduct
    {
        public string PaintingMaterial { get; set; }
        public decimal InchSize { get; set; }   
    }
}
