
namespace MarketPlace.Domain.Models
{
    public class CategoriesSubcategories : Entity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }  
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }    
    }
}
