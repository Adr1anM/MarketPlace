using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ArtMarketPlaceDbContext context) : base(context)
        {
            
        }
        public async Task<List<Author>> GetAllAuthorsByCountry(string country)
            => await _context.Authors.Where(a => a.Country == country).ToListAsync();

        public async Task<Author> GetAuthorByBirthDate(Author author)
            => await _context.Authors.Where(a => a.BirthDate == author.BirthDate).FirstOrDefaultAsync();

        public async Task<Author> GetAuthorByCountry(string country)
            => await _context.Authors.FirstOrDefaultAsync(a => a.Country == country);
    }
}
