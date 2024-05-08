using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using MarketPlace.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArtMarketPlaceDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        public UnitOfWork(ArtMarketPlaceDbContext context, IAuthorRepository authorRepository, 
            IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _repositories = new Dictionary<Type, object>();
            _context = context;
            Authors = authorRepository;
            Orders = orderRepository;
            Products = productRepository;
        }

        public IAuthorRepository Authors { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IProductRepository Products { get; private set; }

        public async Task BeginTransactionAsync()
        {
           await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
           await _context.Database.CommitTransactionAsync();    
        }

        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : Entity
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<TEntity>(_context);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task RollbackTransactionAsync()
        {
           await _context.Database.RollbackTransactionAsync();  
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();

        }


    }
}
