using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUi.Tests.Helpers
{
    public static class CategoriesHelper
    {
        public static void SeedCategories(ArtMarketPlaceDbContext context)
        {
            var categories = new List<Category> 
            {
                new Category { Id = 1, Name = "MockCategory" },
                new Category { Id = 2, Name = "MockCategory2" },
                new Category { Id = 3, Name = "MockCategory2" }
            };
            
            foreach(var category in categories)
            {
                context.Categories.Add(category);   
            }

           
        }
    }
}
