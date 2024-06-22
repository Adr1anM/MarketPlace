using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models;
using System.Linq.Expressions;

namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author> 
    {
        Task<Author> GetAuthorWhere(Expression<Func<Author, bool>> expresionPredicate);
        Task<Author> GetByUserIdWithInclude(int id);
        Task<List<string>> GetAllCountries();
        Task<List<AuthorNameDto>> GetAllAuthorsName();

    }
}
