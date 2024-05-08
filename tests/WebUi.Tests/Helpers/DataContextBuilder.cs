using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebUi.Tests.Helpers
{
    /*public class DataContextBuilder : IDisposable
    {
        private readonly DataContext _dbContext;
        public DataContextBuilder(string dbName = "TestDatabase")
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new DataContext(options);
            _dbContext = context;   
            
        }

        public DataContext GetContext()
        {
            _dbContext.Database.EnsureCreated();    
            return _dbContext;
        }

        public void SeddAuthors(int number = 1)
        {
            var authors = new List<Author>();

            for(int i = 0; i<number; i++)
            {
                var id = i + 1;
                authors.Add(new Author
                {

                    UserId = id,
                    Biography = $"biography id {id}",
                    Country = $"country id {id}",
                    BirthDate = DateTime.Now,
                    SocialMediaLinks = $"link id {id}",
                    NumberOfPosts = id + 4
                });
            }

            _dbContext.AddRange(authors);   
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }*/
}
