using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class Sculpture : BaseArtProduct
    {
        public string Material { get; set; }
        public double Weight { get; set; }  
    }
}
