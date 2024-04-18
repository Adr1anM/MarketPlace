using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ArtMarketPlaceDbContext context) : base(context)
        {
                
        }

        public async Task<Product> GetProductByAuthor(Author author)
        {
            var result =  await _context.Products.FirstOrDefaultAsync(p => p.AuthorId == author.Id);

            if(result is null)
            {
                throw new ValidationException($"Object of type {typeof(Author)} not found");
            }

            return result;
        }

        public async Task<Product> GetProductByCategory(Category category)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.CategoryID == category.Id);
        }

        public Product UpdateCreatedDate(int id,DateTime date)
        {
            var result =  _context.Products.Find(id);

            if(result == null)
            {
                throw new ValidationException($"Object of type {typeof(Author)} with Id:{id} not found");
            }

            result.CreatedDate = date;

            _context.Products.Update(result); 
            return result;  
        }

    }
}
