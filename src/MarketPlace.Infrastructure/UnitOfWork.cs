using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Infrastructure.Persistance.Context;
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
        public UnitOfWork(ArtMarketPlaceDbContext context, IAuthorRepository authorRepository, 
            IOrderRepository orderRepository, IProductRepository productRepository)
        {
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
