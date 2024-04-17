using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class Product : Entity
    {
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
        public int CategorieID { get; set; }
        public Category Category { get; set; } 
        public int AuthorId { get; set; }    
        public Author Author { get; set; }
            
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ProductOrder> ProductOrders { get; } = [];

    }
}
