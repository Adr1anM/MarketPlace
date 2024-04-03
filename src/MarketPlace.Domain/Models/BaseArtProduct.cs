using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class BaseArtProduct 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public decimal Price { get; set; }  
    }
}
