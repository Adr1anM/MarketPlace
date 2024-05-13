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
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; } 
        public int AuthorId { get; set; }    
        public Author Author { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ProductOrder> ProductOrders { get; } = [];

    }
}
