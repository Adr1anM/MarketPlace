using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author> 
    {
        Task<List<Author>> GetAllAuthorsWhere(Expression<Func<Author, bool>> expresionPredicate);
        Task<Author> GetAuthorWhere(Expression<Func<Author, bool>> expresionPredicate);
    }
}
