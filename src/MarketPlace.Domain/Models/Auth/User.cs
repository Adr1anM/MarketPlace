using Microsoft.AspNetCore.Identity;


namespace MarketPlace.Domain.Models.Auth
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public Author Author { get; set; }
    }
}
