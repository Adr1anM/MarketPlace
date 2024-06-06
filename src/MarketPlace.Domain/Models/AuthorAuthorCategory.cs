

namespace MarketPlace.Domain.Models
{
    public class AuthorAuthorCategory : Entity
    {
        public int? AuthorId { get; set; }   
        public Author? Author { get; set; }
        public int? AuthorCategoryId { get; set; }  
        public AuthorCategory? AuthorCategory { get; set;}  
    }
}
