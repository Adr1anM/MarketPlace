using MarketPlace.Domain.Models;


namespace MarketPlace.Domain
{
    public class CategorySubcategory : Entity
    {
        public int CategoryId { get; set; } 
        public Category Category { get; set; } 
        public int SubCategoryId { get; set; } 
        public SubCategory SubCategory { get; set; } 
    }
}
