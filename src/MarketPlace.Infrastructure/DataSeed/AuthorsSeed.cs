using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;


namespace MarketPlace.Infrastructure.DataSeed
{
    public class AuthorsSeed
    {
        public static async Task Seed(ArtMarketPlaceDbContext context)
        {
            if (!context.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author
                    {
                        UserId = 1,
                        Biography = "Biography 1",
                        Country = "USA",
                        BirthDate = new DateTime(1980, 1, 1),
                        SocialMediaLinks = "http://link1.com",
                        NumberOfPosts = 5,
                        PhoneNumber = "1234567890"
                    },
                    new Author
                    {
                        UserId = 2,
                        Biography = "Biography 2",
                        Country = "Canada",
                        BirthDate = new DateTime(1981, 2, 2),
                        SocialMediaLinks = "http://link2.com",
                        NumberOfPosts = 10,
                        PhoneNumber = "0987654321"
                    },
                    new Author
                    {
                        UserId = 3,
                        Biography = "Biography 3",
                        Country = "UK",
                        BirthDate = new DateTime(1982, 3, 3),
                        SocialMediaLinks = "http://link3.com",
                        NumberOfPosts = 15,
                        PhoneNumber = "1111111111"
                    },
                    new Author
                    {
                        UserId = 4,
                        Biography = "Biography 4",
                        Country = "France",
                        BirthDate = new DateTime(1983, 4, 4),
                        SocialMediaLinks = "http://link4.com",
                        NumberOfPosts = 20,
                        PhoneNumber = "2222222222"
                    },
                    new Author
                    {
                        UserId = 5,
                        Biography = "Biography 5",
                        Country = "Germany",
                        BirthDate = new DateTime(1984, 5, 5),
                        SocialMediaLinks = "http://link5.com",
                        NumberOfPosts = 25,
                        PhoneNumber = "3333333333"
                    },
                    new Author
                    {
                        UserId = 6,
                        Biography = "Biography 6",
                        Country = "Italy",
                        BirthDate = new DateTime(1985, 6, 6),
                        SocialMediaLinks = "http://link6.com",
                        NumberOfPosts = 30,
                        PhoneNumber = "4444444444"
                    },
                    new Author
                    {
                        UserId = 7,
                        Biography = "Biography 7",
                        Country = "Spain",
                        BirthDate = new DateTime(1986, 7, 7),
                        SocialMediaLinks = "http://link7.com",
                        NumberOfPosts = 35,
                        PhoneNumber = "5555555555"
                    }
                };
                   
                context.Authors.AddRange(authors);
                await context.SaveChangesAsync();
            }
           
        }
    }
}
