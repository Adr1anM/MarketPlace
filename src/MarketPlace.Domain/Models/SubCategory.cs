using System.ComponentModel.DataAnnotations;


namespace MarketPlace.Domain.Models
{
    public class SubCategory : Entity  
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public List<ProductSubCategory> ProductSubcategories { get; } = [];
        public List<CategorySubcategory> CategorySubcategories { get; set; } = [];

    }
}
