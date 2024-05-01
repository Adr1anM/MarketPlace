using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByAuthorId(int authorId);
        Task<Product> GetProductByCategoryId(int categoryId);
        Product UpdateCreatedDate(int id , DateTime date);
        Task<List<Product>> GetPagedResult(int pageNum, int pagesize);
    }
}
