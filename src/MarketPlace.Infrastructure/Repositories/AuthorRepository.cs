using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ArtMarketPlaceDbContext context) : base(context)
        {
            
        }
        public async Task<List<Author>> GetAllAuthorsWhere(Expression<Func<Author,bool>> expresionPredicate)
            => await _context.Authors.Where(expresionPredicate).ToListAsync();
        
        public async Task<Author> GetAuthorWhere(Expression<Func<Author, bool>> expresionPredicate)
            => await _context.Authors.Where(expresionPredicate).FirstOrDefaultAsync();
    }
}
