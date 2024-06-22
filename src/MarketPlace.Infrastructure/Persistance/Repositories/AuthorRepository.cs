using AutoMapper;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.Common.Models;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace MarketPlace.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ArtMarketPlaceDbContext context) : base(context)
        {
            
        }
      
        public async Task<Author> GetAuthorWhere(Expression<Func<Author, bool>> expresionPredicate)
            => await _context.Authors.FirstOrDefaultAsync(expresionPredicate);

        public async Task<Author> GetByUserIdWithInclude(int id)
           => await _context.Authors.Where(u => u.UserId == id).Include(u => u.User).FirstOrDefaultAsync();

        public async Task<List<string>> GetAllCountries()
        {
            return await _context.Authors
                .Select(c => c.Country)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<AuthorNameDto>> GetAllAuthorsName()
        {
            return await _context.Authors
                .Include(a => a.User)
                .Select(a => new AuthorNameDto
                {
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,

                }).ToListAsync();

        }



        public override Task<PagedResult<TDto>> GetPagedData<TDto>(PagedRequest pagedRequest, IMapper mapper) where TDto : class
        {
            return base.GetPagedData<TDto>(pagedRequest, mapper);
        }
    }
}
