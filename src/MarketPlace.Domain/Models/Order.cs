using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class Order
    {
        public DateTime OrderDate { get; set; }
        public User User {  get; set; }
        public Post post { get; set; }

    }
}
