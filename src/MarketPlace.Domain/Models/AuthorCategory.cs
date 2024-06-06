using System.ComponentModel.DataAnnotations;


namespace MarketPlace.Domain.Models
{
    public class AuthorCategory : Entity
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public List<AuthorAuthorCategory> AuthorAuthorCategories { get; } = [];   

    }
}
