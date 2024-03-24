using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class Post
    {
        public string CategoryId { get; set; }
        public Categories Categories { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PostDate { get; set; }
    }
}
