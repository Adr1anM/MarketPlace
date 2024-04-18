using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductByAuthor(Author author);
        Task<Product> GetProductByCategory(Category category);
        Product UpdateCreatedDate(int id , DateTime date);
    }
}
