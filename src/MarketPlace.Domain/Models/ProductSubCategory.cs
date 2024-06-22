using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class ProductSubCategory : Entity
    {
        public int ProductId { get; set; }  
        public Product Product { get; set; }
        public int SubCategoryId { get; set; }  
        public SubCategory SubCategory { get; set; }  

    }
}
