using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using MarketPlace.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
           await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
           await _context.Database.CommitTransactionAsync(cancellationToken);    
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

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
           await _context.Database.RollbackTransactionAsync(cancellationToken);  
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

        }


    }
}
