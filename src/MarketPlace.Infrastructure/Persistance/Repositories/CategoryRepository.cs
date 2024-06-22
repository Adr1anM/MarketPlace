using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using MarketPlace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Persistance.Repositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(ArtMarketPlaceDbContext context) : base(context)
        {
            
        }

        public async Task<List<CategWithSubCategDto>> GetCategoriesWithSubcategoriesAsync()
        {
            var result = await _context.Categories
                .Include(c => c.CategorySubcategories)
                .ThenInclude(cs => cs.SubCategory)
                .Select(c => new CategWithSubCategDto
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                    SubCategories = c.CategorySubcategories.Select(cs => new SubCategoryDto
                    {
                        Id = cs.SubCategory.Id,
                        Name = cs.SubCategory.Name
                    }).ToList()
                }).ToListAsync();

            return result;
        }


    }
}
