using AutoMapper;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.Common.Models;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using MarketPlace.Application.Extensions;
using System.ComponentModel.DataAnnotations;


namespace MarketPlace.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ArtMarketPlaceDbContext context) : base(context)
        {

        }

        public async Task<Product> GetProductByAuthorId(int authorId)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.AuthorId == authorId);

            if (result is null)
            {
                throw new ValidationException($"Object of type {typeof(Author)} not found");
            }

            return result;
        }

        public async Task<Product> GetProductByCategoryId(int categoryId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.CategoryID == categoryId);
        }

        public Product UpdateCreatedDate(int id, DateTime date)
        {
            var result = _context.Products.Find(id);

            if (result == null)
            {
                throw new ValidationException($"Object of type {typeof(Author)} with Id:{id} not found");
            }

            result.CreatedDate = date;

            _context.Products.Update(result);
            return result;
        }

        public async Task<List<Product>> GetPagedResult(int pageNumb, int pagesize)
        {
            return await _context.Products
                    .Skip((pageNumb - 1) * pagesize)
                    .OrderBy(a => a.Id)
                    .Take(pagesize)
                    .ToListAsync();
        }


        public async Task<List<Product>> GetAllProductsWithAuthorsAndUsersAsync()
        {
            return await _context.Products
                                 .Include(p => p.Author)
                                    .ThenInclude(a => a.User)
                                 .ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsWithAuthorsAndUsersAsync(int id)
        {
            return await _context.Products.Where(product => product.AuthorId == id)
                                        .Include(p => p.Author)
                                            .ThenInclude(a => a.User)
                                         .Include(p => p.ProductSubcategories)
                                             .ThenInclude(a => a.SubCategory)
                                        .ToListAsync();
        }
        public async Task<PagedResult<ProductDto>> GetPagedProductsWithAuthorsAndUsersAsync(int id, PagedRequest pagedRequest, IMapper mapper)
        {
            IQueryable<Product> query = _context.Products.Where(product => product.AuthorId == id)
                                         .Include(p => p.Author)
                                            .ThenInclude(a => a.User)
                                         .Include(p => p.ProductSubcategories)
                                             .ThenInclude(a => a.SubCategory);

            return await query.CreatePaginatedResultAsync<Product, ProductDto>(pagedRequest, mapper);
        }

        public override Task<PagedResult<TDto>> GetPagedData<TDto>(PagedRequest pagedRequest, IMapper mapper) where TDto : class
        {
            return base.GetPagedData<TDto>(pagedRequest, mapper);
        }
    }
}
