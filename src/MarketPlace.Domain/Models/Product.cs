using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Price { get; set; }
        public byte[]? ImageData { get; set; }   
        public DateTime CreatedDate { get; set; }
        public List<ProductOrder> ProductOrders { get; } = [];
        public List<ProductSubCategory> ProductSubcategories { get; } = [];

    }
}
