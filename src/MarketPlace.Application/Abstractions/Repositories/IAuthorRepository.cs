using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface IAuthorRepository
    {

        Task<Author> GetAuthorByCountry(string country);
        Task<List<Author>> GetAllAuthorsByCountry(string country);
        Task<Author> GetAuthorByBirthDate(Author author);
    }
}
