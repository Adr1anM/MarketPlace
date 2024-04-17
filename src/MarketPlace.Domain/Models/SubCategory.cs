using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class SubCategory : Entity  
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public List<CategoriesSubcategories> CategoriesSubcategories { get; } = [];
    }
}
