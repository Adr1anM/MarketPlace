using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUi.Tests.Helpers
{
    public static class AuthorsHelper
    {
        public static void SeedAuthors(ArtMarketPlaceDbContext context)
        {
            var authors = GetSampleAuthors();
            context.Authors.AddRange(authors);
        }

        public static IEnumerable<AuthorDto> GetAllSeededAuthors()
        {
            var auhtors = GetSampleAuthors();
            return auhtors.Select(auhtor => new AuthorDto
            {
                Id = auhtor.Id,
                UserId = auhtor.UserId,
                Biography = auhtor.Biography,
                BirthDate = auhtor.BirthDate,
                SocialMediaLinks = auhtor.SocialMediaLinks,
                NumberOfPosts = auhtor.NumberOfPosts,
            }).ToList();
        }

        private static IEnumerable<Author> GetSampleAuthors()
        {
           
            return new List<Author>
            {
                new Author
                {
                    Id = 1,
                    UserId = 2,
                    Biography = "Biography",
                    Country = "Country",
                    BirthDate = DateTime.Now,
                    SocialMediaLinks = "SocialMediaLinks",
                    NumberOfPosts = 3
                },

                new Author
                {
                    Id = 2,
                    UserId = 2,
                    Biography = "Biography2",
                    Country = "Germany",
                    BirthDate = DateTime.Now,
                    SocialMediaLinks = "SocialMedia2",
                    NumberOfPosts = 2
                },
                new Author
                {
                    Id = 3,
                    UserId = 4,
                    Biography = "Biography3",
                    Country = "France",
                    BirthDate = DateTime.Now,
                    SocialMediaLinks = "SocialMedia3",
                    NumberOfPosts = 9
                },

            };
        }
    }
}
