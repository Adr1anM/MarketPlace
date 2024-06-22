using System.ComponentModel.DataAnnotations;


namespace MarketPlace.Domain.Models
{
    public class Category : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public List<CategorySubcategory> CategorySubcategories { get; set; } = [];
    }
}
