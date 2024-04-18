using MarketPlace.Application.Abstractions.Repositories;
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
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
