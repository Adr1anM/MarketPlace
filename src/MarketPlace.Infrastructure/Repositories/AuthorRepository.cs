using AutoMapper;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.Common.Models;
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
            => await _context.Authors.FirstOrDefaultAsync(expresionPredicate);

        public async Task<List<Author>> GetPagedResult(int pageNumb, int pagesize)
        {
            return await _context.Authors
                    .Skip((pageNumb - 1) * pagesize)
                    .OrderBy(a => a.Id)
                    .Take(pagesize)
                    .ToListAsync();
        }

        public override Task<PagedResult<TDto>> GetPagedData<TDto>(PagedRequest pagedRequest, IMapper mapper) where TDto : class
        {
            return base.GetPagedData<TDto>(pagedRequest, mapper);
        }
    }
}
