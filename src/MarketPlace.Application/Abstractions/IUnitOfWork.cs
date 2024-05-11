using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions
{
    public interface IUnitOfWork
    {
        public IAuthorRepository Authors { get; }
        public IOrderRepository Orders { get; } 
        public IProductRepository Products { get; }

        IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : Entity;
        Task SaveAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);

    }
}
